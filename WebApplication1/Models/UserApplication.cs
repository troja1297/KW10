using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Models
{
    public class UserApplication : IdentityUser
    {
        public List<Comment> Comments { get; set; }
        public List<Picture> Pictures { get; set; }
        public List<Company> Companies { get; set; }
    }
}
