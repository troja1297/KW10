using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApplication1.Models
{
    public class Comment : Entity
    {
        public string Body { get; set; }

        public int Rating  { get; set; }

        public DateTime Date { get; set; }

        public string UserId { get; set; }

        public string CompanyId { get; set; }
    }
}
