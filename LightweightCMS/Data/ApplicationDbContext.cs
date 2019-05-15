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
        public DbSet<Page> Page { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

    }
}
