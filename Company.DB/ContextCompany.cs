using Company.DB.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DB
{
    public class ContextCompany : DbContext
    {
        public ContextCompany(DbContextOptions<ContextCompany> options) : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Worker> Workers { get; set; }

    }
}
