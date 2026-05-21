using AzucareraPomalca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzucareraPomalca.Infrastructure.Configurations
{
    public class GradoDominioConfiguration : IEntityTypeConfiguration<GradoDominio>
    {
        public void Configure(EntityTypeBuilder<GradoDominio> builder)
        {
            builder.ToTable("grado_dominio");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_grado_dominio");
            builder.Property(t => t.Nivel).HasColumnName("nivel");
            builder.Property(t => t.Descripcion).HasColumnName("descripcion");
            builder.Property(t => t.HorasRequeridas).HasColumnName("horas_requeridas").HasDefaultValue(0);
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            builder.Property(t => t.State).HasColumnName("state");
            builder.Property(t => t.IdCompetencia).HasColumnName("id_competencia");

            builder.HasOne(one => one.CompetenciaSimple).WithMany(many => many.GradoDominios).HasForeignKey(fk => fk.IdCompetencia);
        }
    }
}
