using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebApplication1.Models.ViewModels
{
    public class CreateCommentViewModel
    {
        [Required]
        public string Body { get; set; }

        [Required]
        public int Rating { get; set; }

        public string UserId { get; set; }
        public string CompanyId { get; set; }

        [Required]
        public IFormFile File { get; set; }
    }
}
