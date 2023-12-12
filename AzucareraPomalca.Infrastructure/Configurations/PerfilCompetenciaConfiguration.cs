using AzucareraPomalca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzucareraPomalca.Infrastructure.Configurations
{
    public class PerfilCompetenciaConfiguration : IEntityTypeConfiguration<PerfilCompetencia>
    {
        public void Configure(EntityTypeBuilder<PerfilCompetencia> builder)
        {
            builder.ToTable("perfil_competencia");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_perfil_competencia");
            builder.Property(t => t.IdGradoDominio).HasColumnName("id_grado_dominio");
            builder.Property(t => t.IdPuesto).HasColumnName("id_puesto");
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            builder.Property(t => t.State).HasColumnName("state");

            builder.HasOne(one => one.GradoDominio).WithMany(many => many.PerfilCompetencias).HasForeignKey(fk => fk.IdGradoDominio);
            builder.HasOne(one => one.Puesto).WithMany(many => many.PerfilCompetencias).HasForeignKey(fk => fk.IdPuesto);
        }
    }
}
