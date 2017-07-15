using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebConferenceService
{
    [DataContract]
    public class Message
    {
        private String _content;

        [DataMember]
        public String Content
        {
            get => _content;
            set => _content = value;
        }
    }
}
