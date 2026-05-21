using AzucareraPomalca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzucareraPomalca.Infrastructure.Configurations
{
    public class PuestoCursoConfiguration : IEntityTypeConfiguration<PuestoCurso>
    {
        public void Configure(EntityTypeBuilder<PuestoCurso> builder)
        {
            builder.ToTable("puesto_curso");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_puesto_curso");
            builder.Property(t => t.IdCurso).HasColumnName("id_curso");
            builder.Property(t => t.IdPuesto).HasColumnName("id_puesto");
            builder.Property(t => t.HorasRequeridas).HasColumnName("horas_requeridas").HasDefaultValue(0);
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            builder.Property(t => t.State).HasColumnName("state");

            builder.HasOne(one => one.Curso).WithMany(many => many.PuestosCursos).HasForeignKey(fk => fk.IdCurso);
            builder.HasOne(one => one.Puesto).WithMany(many => many.PuestosCursos).HasForeignKey(fk => fk.IdPuesto);
        }
    }
}
