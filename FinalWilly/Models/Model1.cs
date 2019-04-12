namespace FinalWilly.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=DBContext")
        {
        }

        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Contactos> Contactos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Contraseña)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Contactos)
                .WithOptional(e => e.Usuario)
                .HasForeignKey(e => e.IdUsuario);

            modelBuilder.Entity<Contactos>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Contactos>()
                .Property(e => e.Correo)
                .IsUnicode(false);
        }
    }
}
