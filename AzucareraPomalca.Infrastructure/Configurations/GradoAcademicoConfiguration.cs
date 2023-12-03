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
    public class GradoAcademicoConfiguration : IEntityTypeConfiguration<GradoAcademico>
    {
        public void Configure(EntityTypeBuilder<GradoAcademico> builder)
        {
            builder.ToTable("grado_academico");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_grado_academico");
            builder.Property(t => t.Descripcion).HasColumnName("descripcion");
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            builder.Property(t => t.State).HasColumnName("state");
        }
    }
}
