using PyDeployer.Common.Models;

namespace PyDeployer.Common.ViewModels
{
    public class DatabaseViewModel
    {
        public long DatabaseId { get; set; }
        
        public long EnvironmentId { get; set; }
        
        public string Name { get; set; }
        
        public DatabaseType Type { get; set; }
        
        public string Host { get; set; }
        
        public string Port { get; set; }
        
        public string User { get; set; }
        
        public string Password { get; set; }
        
    }
}