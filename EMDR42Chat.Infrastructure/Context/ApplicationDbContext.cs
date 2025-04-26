using EMDR42Chat.Domain.Entities;
using EMDR42Chat.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMDR42Chat.Infrastructure.Context;

public class ApplicationDbContext : DbContext
{
    public DbSet<ClientConnectionEntity> ClientConnections { get; set; }
    public DbSet<TherapeftClients> TherapeftClients { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.Migrate();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
        base.OnConfiguring(optionsBuilder);
    }
}
