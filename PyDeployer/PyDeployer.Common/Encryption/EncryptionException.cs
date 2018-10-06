using System;
using System.Collections.Generic;
using System.Text;

namespace PyDeployer.Common.Encryption
{
    public class EncryptionException : Exception
    {

        public EncryptionException(string message) : base(message) { }

        public EncryptionException(string message, Exception innerException) :
            base(message, innerException) { }

    }
}
