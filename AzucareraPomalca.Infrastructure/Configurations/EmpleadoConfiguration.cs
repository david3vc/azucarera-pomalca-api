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
            builder.Property(t => t.NumeroDocumento).HasColumnName("numero_documento");
            builder.Property(t => t.AppellidoPaterno).HasColumnName("apellido_paterno");
            builder.Property(t => t.AppellidoMaterno).HasColumnName("apellido_materno");
            builder.Property(t => t.InicioPeriodo).HasColumnName("inicio_periodo");
            builder.Property(t => t.IdCondicionEmpleado).HasColumnName("id_condicion_empleado");
            builder.Property(t => t.IdPuesto).HasColumnName("id_puesto");
            builder.Property(t => t.IdEstadoCivil).HasColumnName("id_estado_civil");
            builder.Property(t => t.IdSexo).HasColumnName("id_sexo");
            builder.Property(t => t.IdTipoDocumentoIdentidad).HasColumnName("id_tipo_documento_identidad");
            builder.Property(t => t.FechaNacimiento).HasColumnName("fecha_nacimiento");
            builder.Property(t => t.Direccion).HasColumnName("direccion");
            builder.Property(t => t.FinPeriodo).HasColumnName("fin_periodo");
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            builder.Property(t => t.State).HasColumnName("state");

            builder.HasOne(one => one.Puesto).WithMany(many => many.Empleados).HasForeignKey(fk => fk.IdPuesto);
            builder.HasOne(one => one.CondicionEmpleado).WithMany(many => many.Empleados).HasForeignKey(fk => fk.IdCondicionEmpleado);
            builder.HasOne(one => one.EstadoCivil).WithMany(many => many.EmpleadosEstadoCivil).HasForeignKey(fk => fk.IdEstadoCivil);
            builder.HasOne(one => one.Sexo).WithMany(many => many.EmpleadosSexo).HasForeignKey(fk => fk.IdSexo);
            builder.HasOne(one => one.TipoDocumentoIdentidad).WithMany(many => many.EmpleadosTipoDocumentoIdentidad).HasForeignKey(fk => fk.IdTipoDocumentoIdentidad);
        }
    }
}
