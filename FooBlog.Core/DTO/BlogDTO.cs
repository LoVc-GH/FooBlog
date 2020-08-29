using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FooBlog.Core.DTO
{
    public class BlogDTO
    {
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime CreatedOn { get; set; }
        [Required]
        public string Post { get; set; }
        [Required]
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
