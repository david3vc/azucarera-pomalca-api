using AzucareraPomalca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzucareraPomalca.Infrastructure.Configurations
{
    public class NivelConfiguration : IEntityTypeConfiguration<Nivel>
    {
        public void Configure(EntityTypeBuilder<Nivel> builder)
        {
            builder.ToTable("nivel");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_nivel");
            builder.Property(t => t.Descripcion).HasColumnName("descripcion");
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            builder.Property(t => t.State).HasColumnName("state");
        }
    }
}
