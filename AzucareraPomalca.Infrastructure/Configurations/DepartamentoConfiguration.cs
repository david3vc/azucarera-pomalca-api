using AzucareraPomalca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzucareraPomalca.Infrastructure.Configurations
{
    public class DepartamentoConfiguration : IEntityTypeConfiguration<Departamento>
    {
        public void Configure(EntityTypeBuilder<Departamento> builder)
        {
            builder.ToTable("departamento");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_departamento");
            builder.Property(t => t.Nombre).HasColumnName("nombre");
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            builder.Property(t => t.State).HasColumnName("state");
            builder.Property(t => t.IdGerencia).HasColumnName("id_gerencia");
            builder.Property(t => t.IdDivision).HasColumnName("id_division");

            builder.HasOne(one => one.Gerencia).WithMany(many => many.Departamentos).HasForeignKey(fk => fk.IdGerencia);
            builder.HasOne(one => one.Division).WithMany(many => many.Departamentos).HasForeignKey(fk => fk.IdDivision);
        }
    }
}
