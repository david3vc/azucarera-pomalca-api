using AzucareraPomalca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzucareraPomalca.Infrastructure.Configurations
{
    public class EsfuerzoRequeridoPuestoConfiguration : IEntityTypeConfiguration<EsfuerzoRequeridoPuesto>
    {
        public void Configure(EntityTypeBuilder<EsfuerzoRequeridoPuesto> builder)
        {
            builder.ToTable("esfuerzo_requerido_puesto");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_esfuerzo_requerido_puesto");
            builder.Property(t => t.IdEsfuerzoRequerido).HasColumnName("id_esfuerzo_requerido");
            builder.Property(t => t.IdNivel).HasColumnName("id_nivel");
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            builder.Property(t => t.State).HasColumnName("state");
            builder.Property(t => t.IdPuesto).HasColumnName("id_puesto");

            builder.HasOne(one => one.EsfuerzoRequerido).WithMany(many => many.EsfuerzoRequeridoPuestos).HasForeignKey(fk => fk.IdEsfuerzoRequerido);
            builder.HasOne(one => one.Nivel).WithMany(many => many.EsfuerzoRequeridoPuestos).HasForeignKey(fk => fk.IdNivel);
            builder.HasOne(one => one.Puesto).WithMany(many => many.EsfuerzoRequeridoPuestos).HasForeignKey(fk => fk.IdPuesto);
        }
    }
}
