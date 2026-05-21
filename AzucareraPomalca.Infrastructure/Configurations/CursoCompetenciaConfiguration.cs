using AzucareraPomalca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzucareraPomalca.Infrastructure.Configurations
{
    public class CursoCompetenciaConfiguration : IEntityTypeConfiguration<CursoCompetencia>
    {
        public void Configure(EntityTypeBuilder<CursoCompetencia> builder)
        {
            builder.ToTable("curso_competencia");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_curso_competencia");
            builder.Property(t => t.IdCurso).HasColumnName("id_curso");
            builder.Property(t => t.IdCompetencia).HasColumnName("id_competencia");
            builder.Property(t => t.Factor).HasColumnName("factor").HasPrecision(5, 2).HasDefaultValue(1m);
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            builder.Property(t => t.State).HasColumnName("state");

            builder.HasIndex(t => new { t.IdCurso, t.IdCompetencia }).IsUnique();

            builder.HasOne(one => one.Curso).WithMany(many => many.CursoCompetencias).HasForeignKey(fk => fk.IdCurso);
            builder.HasOne(one => one.Competencia).WithMany(many => many.CursoCompetencias).HasForeignKey(fk => fk.IdCompetencia);
        }
    }
}
