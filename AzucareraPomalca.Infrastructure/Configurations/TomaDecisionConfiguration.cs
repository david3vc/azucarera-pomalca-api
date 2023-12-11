using AzucareraPomalca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzucareraPomalca.Infrastructure.Configurations
{
    public class TomaDecisionConfiguration : IEntityTypeConfiguration<TomaDecision>
    {
        public void Configure(EntityTypeBuilder<TomaDecision> builder)
        {
            builder.ToTable("toma_decision");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_toma_decision");
            builder.Property(t => t.Descripcion).HasColumnName("descripcion");
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            builder.Property(t => t.State).HasColumnName("state");
            builder.Property(t => t.IdTipoTomaDecision).HasColumnName("id_tipo_toma_decision");

            builder.HasOne(one => one.TipoTomaDecision).WithMany(many => many.TomaDecisiones).HasForeignKey(fk => fk.IdTipoTomaDecision);
        }
    }
}
