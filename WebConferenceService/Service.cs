using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WebConferenceService
{
    public class Service : IService
    {
        #region Event
        internal static event ConnectionEventHandler ConnectionEvent;
        internal delegate void ConnectionEventHandler(object sender, ConnectionEventArgs e);

        internal static event DisconnectionEventHandler DisconnectionEvent;
        internal delegate void DisconnectionEventHandler(object sender, DisconnectionEventArgs e);

        internal static event MessageEventHandler MessageEvent;
        internal delegate void MessageEventHandler(object sender, MessageEventArgs e);
        #endregion Event

        #region EventArgs
        internal class ConnectionEventArgs : EventArgs
        {
            public Client Client { get; set; }
        }

        internal class DisconnectionEventArgs : EventArgs
        {
            public Client Client { get; set; }
        }

        internal class MessageEventArgs : EventArgs
        {
            public Client Client { get; set; }
            public Message Message { get; set; }
        }
        #endregion EventArgs

        private List<Client> _clients = new List<Client>();
        private IServiceCallback _callback = null;
        private Client _client;
        private Object thisLock = new Object(); // https://docs.microsoft.com/fr-fr/dotnet/csharp/language-reference/keywords/lock-statement

        public void StartSession(Client client)
        {
            Console.WriteLine("{0} try to start a session", client.Name);

            this._callback = OperationContext.Current.GetCallbackChannel<IServiceCallback>();
            this._client = client;

            lock (thisLock)
            {
                this._clients.Add(client);

                ConnectionEvent += ConnectionHandler;
                DisconnectionEvent += DisconnectionHandler;
                MessageEvent += MessageHandler;

                // All clients are informed of a new client's connection
                var args = new ConnectionEventArgs()
                {
                    Client = this._client
                };
                ConnectionEvent(this, args);

                OperationContext.Current.Channel.Closing += new EventHandler(Channel_Closing);
            }

            Console.WriteLine("{0} is connected", this._client.Name);
        }

        private void Channel_Closing(object sender, EventArgs e)
        {
            Console.WriteLine("{0} has closed his channel", this._client.Name);

            lock (thisLock)
            {
                if (this._clients.Contains(this._client))
                {
                    CloseSession();
                }
            }
        }

        public void CloseSession()
        {
            lock (thisLock)
            {
                ConnectionEvent -= ConnectionHandler;
                DisconnectionEvent -= DisconnectionHandler;
                MessageEvent -= MessageHandler;

                this._clients.Remove(this._client);
            }

            // All Clients are informed of the disconnection of this client
            var args = new DisconnectionEventArgs()
            {
                Client = this._client
            };
            DisconnectionEvent?.Invoke(this, args);

            Console.WriteLine("{0} has closed his session", this._client.Name);
        }

        public void SendMessage(Message message)
        {
            var args = new MessageEventArgs()
            {
                Client = this._client,
                Message = message
            };

            MessageEvent(this, args);

            Console.WriteLine("{0} has sent the message : {1}", this._client.Name, message.Content);
        }

        #region Handler
        private void ConnectionHandler(object sender, ConnectionEventArgs e)
        {
            this._callback.ConnectionClient(e.Client);
        }

        private void DisconnectionHandler(object sender, DisconnectionEventArgs e)
        {
            this._callback.DisconnectionClient(e.Client);
        }

        private void MessageHandler(object sender, MessageEventArgs e)
        {
            this._callback.ReceiveMessage(e.Client, e.Message);
        }
        #endregion Handler
    }
}
