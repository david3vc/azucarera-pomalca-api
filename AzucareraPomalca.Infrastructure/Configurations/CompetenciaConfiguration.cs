using AzucareraPomalca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzucareraPomalca.Infrastructure.Configurations
{
    public class CompetenciaConfiguration : IEntityTypeConfiguration<Competencia>
    {
        public void Configure(EntityTypeBuilder<Competencia> builder)
        {
            builder.ToTable("competencia");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_competencia");
            builder.Property(t => t.Nombre).HasColumnName("nombre");
            builder.Property(t => t.Descripcion).HasColumnName("descripcion");
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            builder.Property(t => t.State).HasColumnName("state");
            builder.Property(t => t.IdTipoCompetencia).HasColumnName("id_tipo_competencia");

            builder.HasOne(one => one.TipoCompetencia).WithMany(many => many.Competencias).HasForeignKey(fk => fk.IdTipoCompetencia);
        }
    }
}
