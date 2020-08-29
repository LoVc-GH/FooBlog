using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FooBlog.Core.Entities
{
    public class BlogCategory
    {
        [Key]
        public int BlogID { get; set; }
        public Blog Blog{ get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
    }
}
