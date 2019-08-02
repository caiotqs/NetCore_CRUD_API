using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GENERIC_CRUD_API.Model
{
    public class ReferenceContext : DbContext
    {
        public ReferenceContext(DbContextOptions<ReferenceContext> options)
            : base(options)
        {
        }

        public DbSet<ReferenceModel> ReferenceItems { get; set; }
    }
}
