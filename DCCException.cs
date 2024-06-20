using System;
using System.Runtime.Serialization;

namespace CRA.ModelLayer.DCC
{
    [Serializable]
    internal class DCCException : Exception
    {
        public DCCException()
        {
        }

        public DCCException(string message) : base(message)
        {
        }

        public DCCException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DCCException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}