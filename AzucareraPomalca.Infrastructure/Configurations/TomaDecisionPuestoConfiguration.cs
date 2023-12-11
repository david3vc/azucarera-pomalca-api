using AzucareraPomalca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzucareraPomalca.Infrastructure.Configurations
{
    public class TomaDecisionPuestoConfiguration : IEntityTypeConfiguration<TomaDecisionPuesto>
    {
        public void Configure(EntityTypeBuilder<TomaDecisionPuesto> builder)
        {
            builder.ToTable("toma_decision_puesto");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_toma_decision_puesto");
            builder.Property(t => t.IdPuesto).HasColumnName("id_puesto");
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            builder.Property(t => t.State).HasColumnName("state");
            builder.Property(t => t.IdNivel).HasColumnName("id_nivel");
            builder.Property(t => t.IdTomaDecision).HasColumnName("id_toma_decision");

            builder.HasOne(one => one.Puesto).WithMany(many => many.TomaDecisionPuestos).HasForeignKey(fk => fk.IdPuesto);
            builder.HasOne(one => one.Nivel).WithMany(many => many.TomaDecisionPuestos).HasForeignKey(fk => fk.IdNivel);
            builder.HasOne(one => one.TomaDecision).WithMany(many => many.TomaDecisionPuestos).HasForeignKey(fk => fk.IdTomaDecision);
        }
    }
}
