using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using SIREON;

namespace SIREON.Models
{
    public class UserViewModels
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public ICollection<AspNetRole> Role { get; set; }
        public string RoleId { get; set; }

        public string UserName { get; set; }

    }
}