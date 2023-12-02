using AzucareraPomalca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzucareraPomalca.Infrastructure.Configurations
{
    public class CoordinacionConfiguration : IEntityTypeConfiguration<Coordinacion>
    {
        public void Configure(EntityTypeBuilder<Coordinacion> builder)
        {
            builder.ToTable("coordinacion");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_coordinacion");
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            builder.Property(t => t.State).HasColumnName("state");
            builder.Property(t => t.IdPuestoCoordinador).HasColumnName("id_puesto_coordinador");
            builder.Property(t => t.IdPuestoCoordinado).HasColumnName("id_puesto_coordinado");

            builder.HasOne(one => one.PuestoCoordinador).WithMany(many => many.CoordinacionesPuestoCoordinador).HasForeignKey(fk => fk.IdPuestoCoordinador);
            builder.HasOne(one => one.PuestoCoordinado).WithMany(many => many.CoordinacionesPuestoCoordinado).HasForeignKey(fk => fk.IdPuestoCoordinado);
        }
    }
}
