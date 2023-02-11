using System;
using System.Collections.Generic;
using System.Text;
using eCommerce.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Products>Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
