namespace BookApp.Data.Configuration
{
    using BookApp.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ReadingHistoryConfiguration : IEntityTypeConfiguration<ReadingHistory>
    {
        public void Configure(EntityTypeBuilder<ReadingHistory> builder)
        {
            throw new NotImplementedException();
        }
    }
}
