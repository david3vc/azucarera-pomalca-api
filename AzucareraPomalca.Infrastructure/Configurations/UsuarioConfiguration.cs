using AzucareraPomalca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzucareraPomalca.Infrastructure.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("usuario");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_usuario");
            builder.Property(t => t.Correo).HasColumnName("correo");
            builder.Property(t => t.Clave).HasColumnName("clave");
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            builder.Property(t => t.State).HasColumnName("state");
            builder.Property(t => t.IdRol).HasColumnName("id_rol");
            builder.Property(t => t.Nombres).HasColumnName("nombres");
            builder.Property(t => t.Apellidos).HasColumnName("apellidos");

            builder.HasOne(one => one.Rol).WithMany(many => many.Usuarios).HasForeignKey(fk => fk.IdRol);
        }
    }
}
