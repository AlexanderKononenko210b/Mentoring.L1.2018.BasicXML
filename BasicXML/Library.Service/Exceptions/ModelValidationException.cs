using System;
using System.Runtime.Serialization;

namespace Library.Service.Exceptions
{
    /// <summary>
    /// Represents a <see cref="ModelValidationException"/> class.
    /// </summary>
    [Serializable]
    public class ModelValidationException : Exception
    {
        public ModelValidationException()
        {
        }

        public ModelValidationException(string message)
            : base(message)
        {
        }

        public ModelValidationException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ModelValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
