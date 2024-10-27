using Animes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Animes.Infra.Data.Mappings
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(p=>p.Id);
            builder.Property(p=>p.Nome)
            .IsRequired()
            .HasMaxLength(250);
            builder.Property(p=>p.Senha)
            .IsRequired()
            .HasMaxLength(32);
            builder.Property(p=>p.UserName)
            .IsRequired()
            .HasMaxLength(50);
            builder.HasIndex(p=>p.UserName)
            .IsUnique();
        }
    }
}