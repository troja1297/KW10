using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Picture : Entity
    {
        public string Path { get; set; }

        public string CommentId { get; set; }

        public string CompanyId { get; set; }
        
        public string UserId { get; set; }
    }
}
