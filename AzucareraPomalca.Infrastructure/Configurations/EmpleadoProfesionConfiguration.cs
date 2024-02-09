using AzucareraPomalca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzucareraPomalca.Infrastructure.Configurations
{
    public class EmpleadoProfesionConfiguration : IEntityTypeConfiguration<EmpleadoProfesion>
    {
        public void Configure(EntityTypeBuilder<EmpleadoProfesion> builder)
        {
            builder.ToTable("empleado_profesion");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_empleado_profesion");
            builder.Property(t => t.IdEmpleado).HasColumnName("id_empleado");
            builder.Property(t => t.IdProfesion).HasColumnName("id_profesion");
            builder.Property(t => t.IdGradoAcademico).HasColumnName("id_grado_academico");
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            builder.Property(t => t.State).HasColumnName("state");

            builder.HasOne(one => one.Empleado).WithMany(many => many.EmpleadoProfesiones).HasForeignKey(fk => fk.IdEmpleado);
            builder.HasOne(one => one.Profesion).WithMany(many => many.EmpleadoProfesiones).HasForeignKey(fk => fk.IdProfesion);
            builder.HasOne(one => one.GradoAcademico).WithMany(many => many.EmpleadoProfesiones).HasForeignKey(fk => fk.IdGradoAcademico);
        }
    }
}
