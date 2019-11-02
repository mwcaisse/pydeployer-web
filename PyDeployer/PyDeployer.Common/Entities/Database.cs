using System;
using OwlTin.Common.Entities;
using PyDeployer.Common.Encryption;
using PyDeployer.Common.Models;

namespace PyDeployer.Common.Entities
{
    public class Database : IActiveEntity, ITrackedEntity, IEncryptionContextEntity
    {

        public long DatabaseId { get; set; }
        
        public long EnvironmentId { get; set; }
       
        public string Name { get; set; }
        
        public DatabaseType Type { get; set; }
        
        [Encrypted]
        public string Host { get; set; }
        
        [Encrypted]
        public string Port { get; set; }
        
        [Encrypted]
        public string User { get; set; }
        
        [Encrypted]
        public string Password { get; set; }
        
        public bool Active { get; set; }
        
        public DateTime CreateDate { get; set; }
        
        public DateTime ModifiedDate { get; set; }
        
        public string EncryptionKey { get; set; }
        
        public virtual Environment Environment { get; set; }
    }
}