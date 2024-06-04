using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MagazineDomain.Model;

namespace MagazineInfrastructure.Data
{
    public class MagazineInfrastructureContext : DbContext
    {
        public MagazineInfrastructureContext (DbContextOptions<MagazineInfrastructureContext> options)
            : base(options)
        {
        }

        public DbSet<MagazineDomain.Model.Author> Author { get; set; } = default!;
    }
}
