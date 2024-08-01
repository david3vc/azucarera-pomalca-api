using AzucareraPomalca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzucareraPomalca.Infrastructure.Configurations
{
    public class CapacitacionConfiguration : IEntityTypeConfiguration<Capacitacion>
    {
        public void Configure(EntityTypeBuilder<Capacitacion> builder)
        {
            builder.ToTable("capacitacion");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_capacitacion");
            builder.Property(t => t.Descripcion).HasColumnName("descripcion");
            builder.Property(t => t.NumeroHoras).HasColumnName("numero_horas");
            builder.Property(t => t.NumeroParticipantes).HasColumnName("numero_participantes");
            builder.Property(t => t.HorasHombre).HasColumnName("horas_hombre");
            builder.Property(t => t.Costo).HasColumnName("costo");
            builder.Property(t => t.CostoXTrabjador).HasColumnName("costo_x_trabajador");
            builder.Property(t => t.CostoXHorasHombre).HasColumnName("costo_x_horas_hombre");
            builder.Property(t => t.IdCurso).HasColumnName("id_curso");
            builder.Property(t => t.IdTipoFacilitador).HasColumnName("id_tipo_facilitador");
            builder.Property(t => t.IdModalidad).HasColumnName("id_modalidad");
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            builder.Property(t => t.State).HasColumnName("state");

            builder.HasOne(one => one.Curso).WithMany(many => many.Capacitaciones).HasForeignKey(fk => fk.IdCurso);
            builder.HasOne(one => one.TipoFacilitador).WithMany(many => many.CapacitacionesTipoFacilitador).HasForeignKey(fk => fk.IdTipoFacilitador);
            builder.HasOne(one => one.Modalidad).WithMany(many => many.CapacitacionesModalidad).HasForeignKey(fk => fk.IdModalidad);
        }
    }
}
