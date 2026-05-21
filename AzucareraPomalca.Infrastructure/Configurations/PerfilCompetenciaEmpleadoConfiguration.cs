using AzucareraPomalca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzucareraPomalca.Infrastructure.Configurations
{
    public class PerfilCompetenciaEmpleadoConfiguration : IEntityTypeConfiguration<PerfilCompetenciaEmpleado>
    {
        public void Configure(EntityTypeBuilder<PerfilCompetenciaEmpleado> builder)
        {
            builder.ToTable("perfil_competencia_empleado");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_perfil_competencia_empleado");
            builder.Property(t => t.IdGradoDominio).HasColumnName("id_grado_dominio").IsRequired(false);
            builder.Property(t => t.IdEmpleado).HasColumnName("id_empleado");
            builder.Property(t => t.IdCompetencia).HasColumnName("id_competencia");
            builder.Property(t => t.HorasReales).HasColumnName("horas_reales").HasPrecision(10, 2).HasDefaultValue(0m);
            builder.Property(t => t.HorasEquivalentes).HasColumnName("horas_equivalentes").HasPrecision(10, 2).HasDefaultValue(0m);
            builder.Property(t => t.FechaCalculo).HasColumnName("fecha_calculo");
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            builder.Property(t => t.State).HasColumnName("state");

            builder.HasIndex(t => new { t.IdEmpleado, t.IdCompetencia }).IsUnique();

            builder.HasOne(one => one.GradoDominio).WithMany(many => many.PerfilCompetenciaEmpleados).HasForeignKey(fk => fk.IdGradoDominio);
            builder.HasOne(one => one.Empleado).WithMany(many => many.PerfilCompetenciaEmpleados).HasForeignKey(fk => fk.IdEmpleado);
            builder.HasOne(one => one.Competencia).WithMany(many => many.PerfilCompetenciaEmpleados).HasForeignKey(fk => fk.IdCompetencia);
        }
    }
}
