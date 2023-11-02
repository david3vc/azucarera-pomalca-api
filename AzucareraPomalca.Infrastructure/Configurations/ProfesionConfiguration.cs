using AzucareraPomalca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzucareraPomalca.Infrastructure.Configurations
{
    public class ProfesionConfiguration : IEntityTypeConfiguration<Profesion>
    {
        public void Configure(EntityTypeBuilder<Profesion> builder)
        {
            builder.ToTable("profesion");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_profesion");
            builder.Property(t => t.Nombre).HasColumnName("nombre");
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            builder.Property(t => t.State).HasColumnName("state");
            builder.Property(t => t.IdTipoProfesion).HasColumnName("id_tipo_profesion");

            builder.HasOne(one => one.TipoProfesion).WithMany(many => many.Profesiones).HasForeignKey(fk => fk.IdTipoProfesion);
        }
    }
}
