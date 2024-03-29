﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhotoStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoStore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Genre> Genres { get; set;  }
        public DbSet<Photo> Photos { get; set; }

       
    }

}
