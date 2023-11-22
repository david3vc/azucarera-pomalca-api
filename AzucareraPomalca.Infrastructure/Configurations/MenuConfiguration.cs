using AzucareraPomalca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzucareraPomalca.Infrastructure.Configurations
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("menu");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_menu");
            builder.Property(t => t.Nombre).HasColumnName("nombre");
            builder.Property(t => t.Orden).HasColumnName("orden");
            builder.Property(t => t.Nivel).HasColumnName("nivel");
            builder.Property(t => t.Icono).HasColumnName("icono");
            builder.Property(t => t.UrlMenu).HasColumnName("url_menu");
            builder.Property(t => t.Visible).HasColumnName("visible");
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            builder.Property(t => t.State).HasColumnName("state");
        }
    }
}
