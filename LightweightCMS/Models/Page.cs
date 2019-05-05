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
        public string Background { get; set; } = null;
        public bool Public { get; set; } = false;
        public int Rows { get; set; } = 1;
        public int Counts { get; set; } = 1;
        public int Gap { get; set; } = 0;
        [Required]
        public IdentityUser User { get; set; }

    }
}
