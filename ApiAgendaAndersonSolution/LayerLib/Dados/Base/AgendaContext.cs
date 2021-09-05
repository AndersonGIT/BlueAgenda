using LayerLib.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerLib.Dados.Base
{
    public class AgendaContext : DbContext
    {
        private readonly string _connectionString;

        public DbSet<ContatoEF> Agenda_Contato { get; set; }

        public AgendaContext()
        {
            _connectionString = "Server=tcp:soupdataserver.database.windows.net,1433;Initial Catalog=mySoupDataBase;Persist Security Info=False;User ID=ssistersentry;Password=ssisterpass12*-;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"; ;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder pDbContextOptionsBuilder)
        {
            pDbContextOptionsBuilder.UseSqlServer(_connectionString);
        }
    }
}