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
    public class MisionConfiguration : IEntityTypeConfiguration<Mision>
    {
        public void Configure(EntityTypeBuilder<Mision> builder)
        {
            builder.ToTable("mision");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_mision");
            builder.Property(t => t.Descripcion).HasColumnName("descripcion");
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            builder.Property(t => t.State).HasColumnName("state");
            builder.Property(t => t.IdPuesto).HasColumnName("id_puesto");

            builder.HasOne(one => one.Puesto).WithMany(many => many.Misiones).HasForeignKey(fk => fk.IdPuesto);
        }
    }
}
