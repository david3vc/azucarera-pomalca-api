using AzucareraPomalca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzucareraPomalca.Infrastructure.Configurations
{
    public class TipoTomaDecisionConfiguration : IEntityTypeConfiguration<TipoTomaDecision>
    {
        public void Configure(EntityTypeBuilder<TipoTomaDecision> builder)
        {
            builder.ToTable("tipo_toma_decision");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_tipo_toma_decision");
            builder.Property(t => t.Descripcion).HasColumnName("descripcion");
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            builder.Property(t => t.State).HasColumnName("state");
        }
    }
}
