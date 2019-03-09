using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.ViewModels
{
    public class DetailsCompanyViewModel
    {
        public string Title { get; set; }
        public string PhotoPath { get; set; }
        public string Id { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        public int CommentCount { get; set; }
        public int PicturesCount { get; set; }
        public string UserId { get; set; }

        public List<Picture> Pictures { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
