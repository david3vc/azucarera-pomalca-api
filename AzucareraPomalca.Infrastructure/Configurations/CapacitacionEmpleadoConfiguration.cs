using AzucareraPomalca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzucareraPomalca.Infrastructure.Configurations
{
    public class CapacitacionEmpleadoConfiguration : IEntityTypeConfiguration<CapacitacionEmpleado>
    {
        public void Configure(EntityTypeBuilder<CapacitacionEmpleado> builder)
        {
            builder.ToTable("capacitacion_empleado");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_capacitacion_empleado");
            builder.Property(t => t.IdCapacitacion).HasColumnName("id_capacitacion");
            builder.Property(t => t.IdEmpleado).HasColumnName("id_empleado");
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            builder.Property(t => t.State).HasColumnName("state");

            builder.HasOne(one => one.Capacitacion).WithMany(many => many.CapacitacionEmpleados).HasForeignKey(fk => fk.IdCapacitacion);
            builder.HasOne(one => one.Empleado).WithMany(many => many.CapacitacionEmpleados).HasForeignKey(fk => fk.IdEmpleado);
        }
    }
}
