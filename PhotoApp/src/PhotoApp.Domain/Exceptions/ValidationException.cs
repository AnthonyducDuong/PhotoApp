﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Domain.Exceptions
{
    [Serializable]
    public class ValidationException : Exception
    {
        public ValidationException() { }

        public ValidationException(string message) : base(message) { }

        public ValidationException(string message, Exception inner) : base(message, inner) { }

        protected ValidationException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext) { }
    }
}
