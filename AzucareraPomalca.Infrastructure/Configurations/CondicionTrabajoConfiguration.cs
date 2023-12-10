using AzucareraPomalca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzucareraPomalca.Infrastructure.Configurations
{
    public class CondicionTrabajoConfiguration : IEntityTypeConfiguration<CondicionTrabajo>
    {
        public void Configure(EntityTypeBuilder<CondicionTrabajo> builder)
        {
            builder.ToTable("condicion_trabajo");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_condicion_trabajo");
            builder.Property(t => t.Descripcion).HasColumnName("descripcion");
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            builder.Property(t => t.State).HasColumnName("state");
            builder.Property(t => t.IdTipoCondicionTrabajo).HasColumnName("id_tipo_condicion_trabajo");

            builder.HasOne(one => one.TipoCondicionTrabajo).WithMany(many => many.CondicionTrabajos).HasForeignKey(fk => fk.IdTipoCondicionTrabajo);
        }
    }
}
