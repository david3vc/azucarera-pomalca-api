using AzucareraPomalca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzucareraPomalca.Infrastructure.Configurations
{
    public class ResponsabilidadPuestoConfiguration : IEntityTypeConfiguration<ResponsabilidadPuesto>
    {
        public void Configure(EntityTypeBuilder<ResponsabilidadPuesto> builder)
        {
            builder.ToTable("responsabilidad_puesto");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_responsabilidad_puesto");
            builder.Property(t => t.IdResponsabilidad).HasColumnName("id_responsabilidad");
            builder.Property(t => t.IdPuesto).HasColumnName("id_puesto");
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            builder.Property(t => t.State).HasColumnName("state");
            builder.Property(t => t.IdNivel).HasColumnName("id_nivel");

            builder.HasOne(one => one.Responsabilidad).WithMany(many => many.ResponsabilidadesPuestos).HasForeignKey(fk => fk.IdResponsabilidad);
            builder.HasOne(one => one.Puesto).WithMany(many => many.ResponsabilidadesPuestos).HasForeignKey(fk => fk.IdPuesto);
            builder.HasOne(one => one.Nivel).WithMany(many => many.ResponsabilidadesPuestos).HasForeignKey(fk => fk.IdNivel);
        }
    }
}
