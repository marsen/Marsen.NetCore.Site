using Microsoft.EntityFrameworkCore;

namespace Marsen.NetCore.DA.Models
{
    public class PhobosContext:DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhobosContext" /> class.
        /// </summary>
        public PhobosContext()
        {
            
        }

        public PhobosContext(DbContextOptions<PhobosContext> options) 
            : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=Phobos;Trusted_Connection=True;");
            }

        }

        public DbSet<Shop> Shop { get; set; }
    }
}
