using AzucareraPomalca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzucareraPomalca.Infrastructure.Configurations
{
    public class TipoEsfuerzoRequeridoConfiguration : IEntityTypeConfiguration<TipoEsfuerzoRequerido>
    {
        public void Configure(EntityTypeBuilder<TipoEsfuerzoRequerido> builder)
        {
            builder.ToTable("tipo_esfuerzo_requerido");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_tipo_esfuerzo_requerido");
            builder.Property(t => t.Descripcion).HasColumnName("descripcion");
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            builder.Property(t => t.State).HasColumnName("state");
        }
    }
}
