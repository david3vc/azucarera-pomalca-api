using AzucareraPomalca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzucareraPomalca.Infrastructure.Configurations
{
    public class EmpleadoCursoConfiguration : IEntityTypeConfiguration<EmpleadoCurso>
    {
        public void Configure(EntityTypeBuilder<EmpleadoCurso> builder)
        {
            builder.ToTable("empleado_curso");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_empleado_curso");
            builder.Property(t => t.IdCurso).HasColumnName("id_curso");
            builder.Property(t => t.IdEmpleado).HasColumnName("id_empleado");
            builder.Property(t => t.HorasAcumuladas).HasColumnName("horas_acumuladas").HasPrecision(10, 2).HasDefaultValue(0m);
            builder.Property(t => t.FechaCalculo).HasColumnName("fecha_calculo");
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            builder.Property(t => t.State).HasColumnName("state");

            builder.HasOne(one => one.Curso).WithMany(many => many.EmpleadoCursos).HasForeignKey(fk => fk.IdCurso);
            builder.HasOne(one => one.Empleado).WithMany(many => many.EmpleadoCursos).HasForeignKey(fk => fk.IdEmpleado);
        }
    }
}
