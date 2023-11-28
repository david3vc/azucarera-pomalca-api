using AzucareraPomalca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzucareraPomalca.Infrastructure.Configurations
{
    public class PermisoConfiguration : IEntityTypeConfiguration<Permiso>
    {
        public void Configure(EntityTypeBuilder<Permiso> builder)
        {
            builder.ToTable("permiso");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_permiso");
            builder.Property(t => t.IdMenu).HasColumnName("id_menu");
            builder.Property(t => t.IdRol).HasColumnName("id_rol");
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            builder.Property(t => t.State).HasColumnName("state");

            builder.HasOne(one => one.Menu).WithMany(many => many.Permisos).HasForeignKey(fk => fk.IdMenu);
            builder.HasOne(one => one.Rol).WithMany(many => many.Permisos).HasForeignKey(fk => fk.IdRol);
        }
    }
}
