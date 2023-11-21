using AzucareraPomalca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzucareraPomalca.Infrastructure.Configurations
{
    public class EmpleadoConfiguration : IEntityTypeConfiguration<Empleado>
    {
        public void Configure(EntityTypeBuilder<Empleado> builder)
        {
            builder.ToTable("empleado");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_empleado");
            builder.Property(t => t.Nombres).HasColumnName("nombres");
            builder.Property(t => t.AppellidoPaterno).HasColumnName("apellido_paterno");
            builder.Property(t => t.AppellidoMaterno).HasColumnName("apellido_materno");
            builder.Property(t => t.InicioPeriodo).HasColumnName("inicio_periodo");
            builder.Property(t => t.IdCondicionEmpleado).HasColumnName("id_condicion_empleado");
            builder.Property(t => t.CodigoCardo).HasColumnName("codigo_cargo");
            builder.Property(t => t.CodigoArea).HasColumnName("codigo_area");
            builder.Property(t => t.IdPuesto).HasColumnName("id_puesto");
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            builder.Property(t => t.State).HasColumnName("state");

            builder.HasOne(one => one.Puesto).WithMany(many => many.Empleados).HasForeignKey(fk => fk.IdPuesto);
        }
    }
}
