using System;
using System.ServiceModel;

namespace WebConferenceService
{
    [ServiceContract(CallbackContract = typeof(IServiceCallback), SessionMode = SessionMode.Required)]
    public interface IService
    {
        [OperationContract(IsInitiating = true, IsTerminating = false, IsOneWay = true)]
        void StartSession(Client client);

        [OperationContract(IsInitiating = false, IsTerminating = true, IsOneWay = true)]
        void CloseSession();

        [OperationContract(IsInitiating = false, IsTerminating = false, IsOneWay = true)]
        void SendMessage(Message message);
    }
}
