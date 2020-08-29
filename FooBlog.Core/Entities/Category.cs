using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace FooBlog.Core.Entities
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<BlogCategory> BlogCategories { get; set; }
    }
}
