using AzucareraPomalca.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzucareraPomalca.Infrastructure.Configurations
{
    public class ExperienciaLaboralConfiguration : IEntityTypeConfiguration<ExperienciaLaboral>
    {
        public void Configure(EntityTypeBuilder<ExperienciaLaboral> builder)
        {
            builder.ToTable("experiencia_laboral");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_experiencia_laboral");
            builder.Property(t => t.Empresa).HasColumnName("empresa");
            builder.Property(t => t.IdEmpleado).HasColumnName("id_empleado");
            builder.Property(t => t.FechaInicio).HasColumnName("fecha_inicio");
            builder.Property(t => t.FechaFin).HasColumnName("fecha_fin");
            builder.Property(t => t.TipoExperiencia).HasColumnName("tipo_experiencia");
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            builder.Property(t => t.State).HasColumnName("state");

            builder.HasOne(one => one.Empleado).WithMany(many => many.ExperienciaLaborales).HasForeignKey(fk => fk.IdEmpleado);
        }
    }
}
