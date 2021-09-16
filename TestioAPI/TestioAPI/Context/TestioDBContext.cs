using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestioAPI.Context
{
    public class TestioDBContext:DbContext
    {
        public TestioDBContext(DbContextOptions<TestioDBContext> options)
        : base(options) { }
      

        public DbSet<Entities.Users> Users { get; set; }
        public DbSet<Entities.Tasks> Tasks { get; set; }

    }
}
