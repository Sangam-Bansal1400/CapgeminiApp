using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapgAppLibrary
{
    public class AuthenticationResponse
    {
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public int userId{ get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }   
    }
}
