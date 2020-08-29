using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FooBlog.Core.Entities
{
    public class Blog
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Post { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        
        public ICollection<BlogCategory> Categories { get; set;  } 
        public ICollection<Comment> Comments { get; set; }
    }
}
