using Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public class CRMDBContext : DbContext
    {
        public CRMDBContext(DbContextOptions<CRMDBContext> options) : base(options) { }

        public DbSet<Lead> Leads { get; set; }

        public DbSet<Core.Task> Tasks { get; set; }
    }
}
