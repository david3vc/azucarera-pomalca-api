using AzucareraPomalca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzucareraPomalca.Infrastructure.Configurations
{
    public class GrupoOcupacionalConfiguration : IEntityTypeConfiguration<GrupoOcupacional>
    {
        public void Configure(EntityTypeBuilder<GrupoOcupacional> builder)
        {
            builder.ToTable("grupo_ocupacional");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_grupo_ocupacional");
            builder.Property(t => t.Nombre).HasColumnName("nombre");
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            builder.Property(t => t.State).HasColumnName("state");
        }
    }
}
