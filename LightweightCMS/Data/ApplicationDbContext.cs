using System;
using System.Collections.Generic;
using System.Text;
using LightweightCMS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LightweightCMS.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        DbSet<Page> Pages { get; set; }
        DbSet<Element> Elements { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
