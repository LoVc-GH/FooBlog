using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FooBlog.Core.Entities
{
    public class Comment
    {
        public int ID { get; set; }
        public string Text { get; set; } 

        public int BlogID { get; set; }
        public virtual Blog Blog { get; set; }
    }
}
