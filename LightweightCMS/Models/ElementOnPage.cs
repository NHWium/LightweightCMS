using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LightweightCMS.Models
{
    public class ElementOnPage
    {
        public int PageId { get; set; }
        public Element Element { get; set; } = new Element();
    }
}
