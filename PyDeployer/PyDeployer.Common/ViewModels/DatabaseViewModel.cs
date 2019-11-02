using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PyDeployer.Common.Models;

namespace PyDeployer.Common.ViewModels
{
    public class DatabaseViewModel
    {
        public long DatabaseId { get; set; }
        
        public long EnvironmentId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [Required]
        public DatabaseType Type { get; set; }
        
        [Required]
        public string Host { get; set; }
        
        [Required]
        public string Port { get; set; }
        
        [Required]
        public string User { get; set; }
        
        [Required]
        public string Password { get; set; }

        
    }
}