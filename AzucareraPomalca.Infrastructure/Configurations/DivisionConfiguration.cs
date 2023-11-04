using AzucareraPomalca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzucareraPomalca.Infrastructure.Configurations
{
    public class DivisionConfiguration : IEntityTypeConfiguration<Division>
    {
        public void Configure(EntityTypeBuilder<Division> builder)
        {
            builder.ToTable("division");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_division");
            builder.Property(t => t.Nombre).HasColumnName("nombre");
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            builder.Property(t => t.State).HasColumnName("state");
            builder.Property(t => t.IdGerencia).HasColumnName("id_gerencia");

            builder.HasOne(one => one.Gerencia).WithMany(many => many.Divisiones).HasForeignKey(fk => fk.IdGerencia);
        }
    }
}
