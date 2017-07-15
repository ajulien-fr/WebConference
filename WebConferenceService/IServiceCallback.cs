using System.ServiceModel;

namespace WebConferenceService
{
    public interface IServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void ConnectionClient(Client client);

        [OperationContract(IsOneWay = true)]
        void DisconnectionClient(Client client);

        [OperationContract(IsOneWay = true)]
        void ReceiveMessage(Client client, Message message);
    }
}