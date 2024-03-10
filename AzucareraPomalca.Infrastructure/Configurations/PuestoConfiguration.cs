using AzucareraPomalca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzucareraPomalca.Infrastructure.Configurations
{
    public class PuestoConfiguration : IEntityTypeConfiguration<Puesto>
    {
        public void Configure(EntityTypeBuilder<Puesto> builder)
        {
            builder.ToTable("puesto");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_puesto");
            builder.Property(t => t.Codigo).HasColumnName("codigo");
            builder.Property(t => t.Nombre).HasColumnName("nombre");
            builder.Property(t => t.AmbienteTrabajo).HasColumnName("ambiente_trabajo");
            builder.Property(t => t.ExperienciaGeneralMinima).HasColumnName("experiencia_general_minima");
            builder.Property(t => t.ExperienciaGeneralPreferencia).HasColumnName("experiencia_general_preferencia");
            builder.Property(t => t.ExperienciaPuestoMinima).HasColumnName("experiencia_puesto_minima");
            builder.Property(t => t.ExperienciaPuestoPreferencia).HasColumnName("experiencia_puesto_preferencia");
            builder.Property(t => t.IdPuestoSupervisor).HasColumnName("id_puesto_supervisor");
            builder.Property(t => t.IdClaseOcupacional).HasColumnName("id_clase_ocupacional");
            builder.Property(t => t.IdGerencia).HasColumnName("id_gerencia");
            builder.Property(t => t.IdDivision).HasColumnName("id_division");
            builder.Property(t => t.IdDepartamento).HasColumnName("id_departamento");
            builder.Property(t => t.IdSeccion).HasColumnName("id_seccion");
            builder.Property(t => t.CodigoCargo).HasColumnName("codigo_cargo");
            builder.Property(t => t.CodigoArea).HasColumnName("codigo_area");
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            builder.Property(t => t.State).HasColumnName("state");

            builder.HasOne(one => one.PuestoSupervisor).WithMany(many => many.PuestosSubalternos).HasForeignKey(fk => fk.IdPuestoSupervisor);
            builder.HasOne(one => one.ClaseOcupacional).WithMany(many => many.Puestos).HasForeignKey(fk => fk.IdClaseOcupacional);
            builder.HasOne(one => one.Gerencia).WithMany(many => many.Puestos).HasForeignKey(fk => fk.IdGerencia);
            builder.HasOne(one => one.Division).WithMany(many => many.Puestos).HasForeignKey(fk => fk.IdDivision);
            builder.HasOne(one => one.Departamento).WithMany(many => many.Puestos).HasForeignKey(fk => fk.IdDepartamento);
            builder.HasOne(one => one.Seccion).WithMany(many => many.Puestos).HasForeignKey(fk => fk.IdSeccion);
        }
    }
}
