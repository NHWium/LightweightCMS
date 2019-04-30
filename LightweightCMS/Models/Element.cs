﻿using System;
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
        [Required]
        public int ElementId { get; } = 0;
        public string Content { get; set; } = null;
        public string Background { get; set; } = null;
        public int? RowStart { get; set; } = null;
        public int? RowEnd { get; set; } = null;
        public int? ColumnStart { get; set; } = null;
        public int? ColumnEnd { get; set; } = null;
        [ForeignKey("Page")]
        [Required]
        public int PageId { get; set; }

        public Element(int elementId)
        {
            ElementId = elementId;
        }
    }
}