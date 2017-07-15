using System;
using System.Runtime.Serialization;

namespace WebConferenceService
{
    [DataContract]
    public class Client
    {
        private String _name;

        [DataMember]
        public String Name
        {
            get => _name;
            set => _name = value;
        }
    }
}
