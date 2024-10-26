using Animes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Animes.Infra.Data.Mappings
{
    public class DiretoresMap : IEntityTypeConfiguration<Diretor>
    {
        public void Configure(EntityTypeBuilder<Diretor> builder)
        {
            builder.HasKey(e=>e.Id);
            builder.Property(p=>p.Nome)
            .HasMaxLength(250);
        }
    }
}