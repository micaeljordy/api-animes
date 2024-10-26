using Animes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Animes.Infra.Data.Mappings
{
    public class AnimesMap : IEntityTypeConfiguration<Anime>
    {
        public void Configure(EntityTypeBuilder<Anime> builder)
        {
            builder.HasKey(p=>p.Id);
            builder.Property(p=>p.Nome)
            .IsRequired()
            .HasMaxLength(250);
            builder.Property(p=>p.Resumo)
            .HasMaxLength(10000);
            builder.HasOne(p=>p.DiretorNavigation)
            .WithMany(p=>p.AnimesNavigations)
            .HasForeignKey(p=>p.IdDiretor);
        }
    }
}