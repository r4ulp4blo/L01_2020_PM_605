using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using L01_2020_PM_605.Models;

namespace L01_2020_PM_605.Models
{
    public class restauranteContext: DbContext
    {
        public restauranteContext(DbContextOptions<restauranteContext> options) : base(options)
        { 

        }

        public DbSet<clientes> clientes { get; set; }

        public DbSet<pedidos> pedidos { get; set; }

        public DbSet<platos> platos { get; set; }

        public DbSet<PedidosRegistro> PedidosRegistro { get; set; }
    }
}
