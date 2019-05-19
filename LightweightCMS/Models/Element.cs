using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LightweightCMS.Models
{
    public class Element
    {
        [Key]
        public int ElementId { get; set; }
        public string Content { get; set; } = "<p>Add content here</p>";
        public string Background { get; set; } = "hsla(0, 0%, 0%, 0.00)";
        public int? RowStart { get; set; } = 0;
        public int? RowEnd { get; set; } = 0;
        public int? ColumnStart { get; set; } = 0;
        public int? ColumnEnd { get; set; } = 0;
        public int PageId { get; set; }
    }
}
