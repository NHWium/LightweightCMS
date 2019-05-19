using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LightweightCMS.Models
{
    public class Page
    {
        [Key]
        public int PageId { get; set; }
        [Required]
        public string Titel { get; set; } = null;
        public string Background { get; set; } = "hsla(0, 0%, 0%, 0.00)";
        public bool Public { get; set; } = false;
        public int Rows { get; set; } = 12;
        public int Columns { get; set; } = 12;
        public int Gap { get; set; } = 0;
        [Required]
        public IdentityUser User { get; set; }
        public List<Element> Elements { get; set; } = new List<Element>();

        public Element GetElement(int? id)
        {
            return Elements.Where(e => e
                .ElementId == id)
                .FirstOrDefault();
        }
        public bool HasElement(int? id)
        {
            return Elements.Any(e => e
                .ElementId == id);
        }
    }
}
