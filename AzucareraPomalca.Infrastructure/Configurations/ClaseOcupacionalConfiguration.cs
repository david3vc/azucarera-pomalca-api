using AzucareraPomalca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzucareraPomalca.Infrastructure.Configurations
{
    public class ClaseOcupacionalConfiguration : IEntityTypeConfiguration<ClaseOcupacional>
    {
        public void Configure(EntityTypeBuilder<ClaseOcupacional> builder)
        {
            builder.ToTable("clase_ocupacional");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_clase_ocupacional");
            builder.Property(t => t.Nombre).HasColumnName("nombre");
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            builder.Property(t => t.State).HasColumnName("state");
            builder.Property(t => t.IdGrupoOcupacional).HasColumnName("id_grupo_ocupacional");

            builder.HasOne(one => one.GrupoOcupacional).WithMany(many => many.ClaseOcupacionales).HasForeignKey(fk => fk.IdGrupoOcupacional);
        }
    }
}
