using AzucareraPomalca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzucareraPomalca.Infrastructure.Configurations
{
    public class CondicionTrabajoPuestoConfiguration : IEntityTypeConfiguration<CondicionTrabajoPuesto>
    {
        public void Configure(EntityTypeBuilder<CondicionTrabajoPuesto> builder)
        {
            builder.ToTable("condicion_trabajo_puesto");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_condicion_trabajo_puesto");
            builder.Property(t => t.IsMarked).HasColumnName("is_marked");
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            builder.Property(t => t.State).HasColumnName("state");
            builder.Property(t => t.IdCondicionTrabajo).HasColumnName("id_condicion_trabajo");
            builder.Property(t => t.IdPuesto).HasColumnName("id_puesto");

            builder.HasOne(one => one.CondicionTrabajo).WithMany(many => many.CondicionTrabajoPuestos).HasForeignKey(fk => fk.IdCondicionTrabajo);
            builder.HasOne(one => one.Puesto).WithMany(many => many.CondicionTrabajoPuestos).HasForeignKey(fk => fk.IdPuesto);
        }
    }
}
