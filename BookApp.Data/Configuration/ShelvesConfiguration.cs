namespace BookApp.Data.Configuration
{
    using BookApp.Data.Models;
    using static Common.EntityValidationConstants.Shelves;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ShelvesConfiguration : IEntityTypeConfiguration<Shelve>
    {
        public void Configure(EntityTypeBuilder<Shelve> builder)
        {
            builder
                .HasKey(s => s.Id);

            builder
                .Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength);

            builder
               .Property(s => s.Description)
               .IsRequired()
               .HasMaxLength(DescriptionMaxLength);
        }
    }
}
