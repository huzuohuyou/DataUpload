using System;
using System.Collections.Generic;
using System.Text;

namespace ToolFunction
{
    [Serializable()]
    public class MessageContent
    {
        private DateTime _creationDate = DateTime.Now;
        private string _messageText;

        public MessageContent()
        {

        }

        public MessageContent(string messageText)
        {
            _messageText = messageText;
        }

        public string MessageText
        {
            get { return _messageText; }
            set { _messageText = value; }
        }

        public DateTime CreationDate
        {
            get { return _creationDate; }
            set { _creationDate = value; }
        }

    }
}
