using AzucareraPomalca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzucareraPomalca.Infrastructure.Configurations
{
    public class FuncionEspecificaConfiguration : IEntityTypeConfiguration<FuncionEspecifica>
    {
        public void Configure(EntityTypeBuilder<FuncionEspecifica> builder)
        {
            builder.ToTable("funcion_especifica");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_funcion_especifica");
            builder.Property(t => t.Descripcion).HasColumnName("descripcion");
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            builder.Property(t => t.State).HasColumnName("state");
            builder.Property(t => t.IdPuesto).HasColumnName("id_puesto");

            builder.HasOne(one => one.Puesto).WithMany(many => many.FuncionesEspecificas).HasForeignKey(fk => fk.IdPuesto);
        }
    }
}
