using AzucareraPomalca.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzucareraPomalca.Infrastructure.Configurations
{
    public class PuestoProfesionConfiguration : IEntityTypeConfiguration<PuestoProfesion>
    {
        public void Configure(EntityTypeBuilder<PuestoProfesion> builder)
        {
            builder.ToTable("puesto_profesion");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_puesto_profesion");
            builder.Property(t => t.IdProfesion).HasColumnName("id_profesion");
            builder.Property(t => t.IdPuesto).HasColumnName("id_puesto");
            builder.Property(t => t.IdGradoAcademico).HasColumnName("id_grado_academico");
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            builder.Property(t => t.State).HasColumnName("state");

            builder.HasOne(one => one.Puesto).WithMany(many => many.PuestosProfesiones).HasForeignKey(fk => fk.IdPuesto);
            builder.HasOne(one => one.Profesion).WithMany(many => many.PuestosProfesiones).HasForeignKey(fk => fk.IdProfesion);
            builder.HasOne(one => one.GradoAcademico).WithMany(many => many.PuestosProfesiones).HasForeignKey(fk => fk.IdGradoAcademico);
        }
    }
}
