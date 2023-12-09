using AzucareraPomalca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzucareraPomalca.Infrastructure.Configurations
{
    public class CursoConfiguration : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.ToTable("curso");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_curso");
            builder.Property(t => t.Codigo).HasColumnName("codigo");
            builder.Property(t => t.Descripcion).HasColumnName("descripcion");
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            builder.Property(t => t.State).HasColumnName("state");
            builder.Property(t => t.IdTipoCurso).HasColumnName("id_tipo_curso");
            builder.Property(t => t.Gerencia).HasColumnName("gerencia");

            builder.HasOne(one => one.TipoCurso).WithMany(many => many.Cursos).HasForeignKey(fk => fk.IdTipoCurso);
        }
    }
}
