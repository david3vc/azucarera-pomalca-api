using AzucareraPomalca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzucareraPomalca.Infrastructure.Configurations
{
    public class SeccionConfiguration : IEntityTypeConfiguration<Seccion>
    {
        public void Configure(EntityTypeBuilder<Seccion> builder)
        {
            builder.ToTable("seccion");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_seccion");
            builder.Property(t => t.Nombre).HasColumnName("nombre");
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            builder.Property(t => t.State).HasColumnName("state");
            builder.Property(t => t.IdDepartamento).HasColumnName("id_departamento");
            builder.Property(t => t.IdGerencia).HasColumnName("id_gerencia");
            builder.Property(t => t.IdDivision).HasColumnName("id_division");

            builder.HasOne(one => one.Departamento).WithMany(many => many.Secciones).HasForeignKey(fk => fk.IdDepartamento);
            builder.HasOne(one => one.Gerencia).WithMany(many => many.Secciones).HasForeignKey(fk => fk.IdGerencia);
            builder.HasOne(one => one.Division).WithMany(many => many.Secciones).HasForeignKey(fk => fk.IdDivision);
        }
    }
}
