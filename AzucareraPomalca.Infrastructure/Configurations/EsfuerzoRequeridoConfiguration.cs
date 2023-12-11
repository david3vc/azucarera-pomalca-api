using AzucareraPomalca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzucareraPomalca.Infrastructure.Configurations
{
    public class EsfuerzoRequeridoConfiguration : IEntityTypeConfiguration<EsfuerzoRequerido>
    {
        public void Configure(EntityTypeBuilder<EsfuerzoRequerido> builder)
        {
            builder.ToTable("esfuerzo_requerido");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_esfuerzo_requerido");
            builder.Property(t => t.Descripcion).HasColumnName("descripcion");
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            builder.Property(t => t.State).HasColumnName("state");
            builder.Property(t => t.IdTipoEsfuerzoRequerido).HasColumnName("id_tipo_esfuerzo_requerido");

            builder.HasOne(one => one.TipoEsfuerzoRequerido).WithMany(many => many.EsfuerzoRequeridos).HasForeignKey(fk => fk.IdTipoEsfuerzoRequerido);
        }
    }
}
