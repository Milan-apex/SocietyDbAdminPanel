using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocietyDBAdminPanel.Core.Models
{
    public class DbConnectionPropModel
    {
        public int Id { get; set; }
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } // Store securely (hashed)
        public string SecurityCode { get; set; }
    }
}
