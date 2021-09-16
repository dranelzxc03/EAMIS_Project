using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMIS.Common
{
    public class UserloginDTO
    {
        public int? UserId { get; set; }
        public int? PermissionId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
