using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Company : Entity
    {
        public string PhotoPath { get; set; }

        public string Title { get; set; }

        public string Descriprion { get; set; }

        public int CommentCount { get; set; }

        public double Rating { get; set; }

        public int PhotoCount { get; set; }

        public string UserId { get; set; }

    }
}