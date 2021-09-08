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
      

        public DbSet<Entities.User> Users { get; set; }


    }
}
