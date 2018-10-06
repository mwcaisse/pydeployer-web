using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace PyDeployer.Common.Encryption
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class EncryptedAttribute : Attribute { }
}
