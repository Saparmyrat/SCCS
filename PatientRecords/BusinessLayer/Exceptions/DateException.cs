using System;
using System.Runtime.Serialization;

namespace BusinessLayer.Exceptions
{
    [Serializable]
    public class DateException : Exception
    {
        public string ParamName { get; set; }

        public DateException()
        {
        }

        public DateException(string message)
            : base(message)
        {
        }

        public DateException(string message, string paramName)
            : base(message)
        {
            if (paramName == null)
            {
                throw new ArgumentNullException(paramName);
            }

            ParamName = paramName;
        }

        public DateException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected DateException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}
