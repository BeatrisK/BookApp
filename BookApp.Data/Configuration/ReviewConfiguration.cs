﻿namespace BookApp.Data.Configuration
{
    using BookApp.Data.Models;
    using static Common.EntityValidationConstants.Review;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder
                .HasKey(r => r.Id);

            builder
                .HasOne(b => b.User)
                .WithMany()
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Property(r => r.ReviewDate)
                .IsRequired();

            builder
                .Property(r => r.ReviewText)
                .IsRequired()
                .HasMaxLength(ReviewTextMaxLength);

            builder
                .Property(r => r.Rating)
                .IsRequired()
                .HasMaxLength(ReviewTextMaxLength);

            builder
                .Property(b => b.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            builder
                .HasOne(r => r.Book)
                .WithMany(b => b.Reviews)
                .HasForeignKey(r => r.BookId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
