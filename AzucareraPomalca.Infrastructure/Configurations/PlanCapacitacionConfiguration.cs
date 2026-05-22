using AzucareraPomalca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzucareraPomalca.Infrastructure.Configurations
{
    public class PlanCapacitacionConfiguration : IEntityTypeConfiguration<PlanCapacitacion>
    {
        public void Configure(EntityTypeBuilder<PlanCapacitacion> builder)
        {
            builder.ToTable("plan_capacitacion");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_plan_capacitacion");
            builder.Property(t => t.Anio).HasColumnName("anio");
            builder.Property(t => t.Descripcion).HasColumnName("descripcion");
            builder.Property(t => t.PresupuestoTotal).HasColumnName("presupuesto_total").HasPrecision(18, 2).HasDefaultValue(0m);
            builder.Property(t => t.FuerzaLaboral).HasColumnName("fuerza_laboral").HasDefaultValue(0);
            builder.Property(t => t.IdEstadoPlan).HasColumnName("id_estado_plan");
            builder.Property(t => t.FechaAprobacion).HasColumnName("fecha_aprobacion");
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            builder.Property(t => t.State).HasColumnName("state");

            // Un plan activo (no eliminado) por año: índice único filtrado por state = 1.
            builder.HasIndex(t => t.Anio).IsUnique().HasFilter("[state] = 1");

            builder.HasOne(one => one.EstadoPlan).WithMany().HasForeignKey(fk => fk.IdEstadoPlan);
        }
    }
}
