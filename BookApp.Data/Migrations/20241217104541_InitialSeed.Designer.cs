﻿// <auto-generated />
using System;
using BookApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookApp.Data.Migrations
{
    [DbContext(typeof(BookDbContext))]
    [Migration("20241217104541_InitialSeed")]
    partial class InitialSeed
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BookApp.Data.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("BookApp.Data.Models.ApplicationUserBook", b =>
                {
                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<bool>("IsWantToRead")
                        .HasColumnType("bit");

                    b.HasKey("ApplicationUserId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("UsersBooks");
                });

            modelBuilder.Entity("BookApp.Data.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "J.K. Rowling"
                        },
                        new
                        {
                            Id = 2,
                            Name = "George R.R. Martin"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Harper Lee"
                        },
                        new
                        {
                            Id = 4,
                            Name = "George Orwell"
                        });
                });

            modelBuilder.Entity("BookApp.Data.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(2083)
                        .HasColumnType("nvarchar(2083)");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("Pages")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Publisher")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AuthorId = 1,
                            Description = "In the first book of the Harry Potter series, Harry discovers that he is a wizard and attends Hogwarts School of Witchcraft and Wizardry. Along with his friends Ron and Hermione, he uncovers secrets about his past and faces the dark wizard Voldemort who killed his parents. This enchanting tale filled with magical creatures, spellbinding adventures, and life-changing moments introduces the iconic characters that will shape Harry's destiny throughout the series.",
                            Genre = "Fantasy",
                            ImageUrl = "https://m.media-amazon.com/images/I/81q77Q39nEL.jpg",
                            IsDeleted = false,
                            Pages = 223,
                            Price = 19.99m,
                            Publisher = "Bloomsbury",
                            Title = "Harry Potter and the Philosopher's Stone"
                        },
                        new
                        {
                            Id = 2,
                            AuthorId = 1,
                            Description = "The second book in the Harry Potter series follows Harry as he returns to Hogwarts for his second year. A mysterious force is petrifying students, and Harry, along with his friends, investigates the legend of the Chamber of Secrets. With the discovery of ancient magical creatures and a sinister plot by the heir of Salazar Slytherin, Harry must confront new challenges that will test his bravery and loyalty.",
                            Genre = "Fantasy",
                            ImageUrl = "https://m.media-amazon.com/images/I/818umIdoruL.jpg",
                            IsDeleted = false,
                            Pages = 251,
                            Price = 20.99m,
                            Publisher = "Bloomsbury",
                            Title = "Harry Potter and the Chamber of Secrets"
                        },
                        new
                        {
                            Id = 3,
                            AuthorId = 2,
                            Description = "\"In this epic first book of the 'A Song of Ice and Fire' series, George R.R. Martin weaves a complex tale of power, betrayal, and intrigue set in the Seven Kingdoms of Westeros. As noble families vie for the Iron Throne, they are forced to confront not only each other but the dangers lurking in the north and beyond the Wall. With vivid characters and morally ambiguous choices, 'A Game of Thrones' sets the stage for a high-stakes political drama that will keep readers hooked.",
                            Genre = "Fantasy",
                            ImageUrl = "https://i.harperapps.com/hcanz/covers/9780007459483/y648.jpg",
                            IsDeleted = false,
                            Pages = 694,
                            Price = 25.99m,
                            Publisher = "Bantam Books",
                            Title = "A Game of Thrones"
                        },
                        new
                        {
                            Id = 4,
                            AuthorId = 2,
                            Description = "The second book in the 'A Song of Ice and Fire' series continues the fierce political and military struggles in Westeros. As the War of the Five Kings rages, alliances are forged and broken, and noble houses are brought to their knees. Meanwhile, darker forces stir beyond the Wall and in the east, where Daenerys Targaryen plots her return to power. With epic battles, unexpected twists, and a wide array of compelling characters.",
                            Genre = "Fantasy",
                            ImageUrl = "https://target.scene7.com/is/image/Target/GUEST_7a799fff-46fc-469d-8689-cbf1e7745fef",
                            IsDeleted = false,
                            Pages = 768,
                            Price = 27.99m,
                            Publisher = "Bantam Books",
                            Title = "A Clash of Kings"
                        },
                        new
                        {
                            Id = 5,
                            AuthorId = 3,
                            Description = "Harper Lee’s classic novel, 'To Kill a Mockingbird,' explores themes of racial injustice, moral growth, and compassion in the Deep South during the 1930s. The story is told through the eyes of Scout Finch, a young girl whose father, lawyer Atticus Finch, defends an African American man falsely accused of raping a white woman.",
                            Genre = "Fiction",
                            ImageUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1612238791i/56916837.jpg",
                            IsDeleted = false,
                            Pages = 281,
                            Price = 18.99m,
                            Publisher = "J.B. Lippincott & Co.",
                            Title = "To Kill a Mockingbird"
                        },
                        new
                        {
                            Id = 6,
                            AuthorId = 4,
                            Description = "George Orwell’s '1984' paints a chilling picture of a totalitarian regime that controls every aspect of life. The story follows Winston Smith, a worker at the Ministry of Truth, who begins to question the oppressive government that monitors all citizens through omnipresent surveillance and mind control.",
                            Genre = "Dystopian",
                            ImageUrl = "https://knigomania.bg/media/catalog/product/cache/02f16ac392ba7c312a70e2f3c5d752a7/1/9/1984-9780451524935.jpg",
                            IsDeleted = false,
                            Pages = 328,
                            Price = 14.99m,
                            Publisher = "Secker & Warburg",
                            Title = "1984"
                        });
                });

            modelBuilder.Entity("BookApp.Data.Models.ReadList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("UserId");

                    b.ToTable("ReadLists");
                });

            modelBuilder.Entity("BookApp.Data.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("Rating")
                        .HasMaxLength(2000)
                        .HasColumnType("int");

                    b.Property<DateTime>("ReviewDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReviewText")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("BookApp.Data.Models.WantToReadList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("UserId");

                    b.ToTable("WantToReadLists");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("BookApp.Data.Models.ApplicationUserBook", b =>
                {
                    b.HasOne("BookApp.Data.Models.ApplicationUser", "ApplicationUser")
                        .WithMany("ApplicationUserBooks")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookApp.Data.Models.Book", "Book")
                        .WithMany("BookApplicationUsers")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("BookApp.Data.Models.Book", b =>
                {
                    b.HasOne("BookApp.Data.Models.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("BookApp.Data.Models.ReadList", b =>
                {
                    b.HasOne("BookApp.Data.Models.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookApp.Data.Models.ApplicationUser", "User")
                        .WithMany("ReadLists")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BookApp.Data.Models.Review", b =>
                {
                    b.HasOne("BookApp.Data.Models.Book", "Book")
                        .WithMany("Reviews")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookApp.Data.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BookApp.Data.Models.WantToReadList", b =>
                {
                    b.HasOne("BookApp.Data.Models.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookApp.Data.Models.ApplicationUser", "User")
                        .WithMany("WantToReadLists")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("BookApp.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("BookApp.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookApp.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("BookApp.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BookApp.Data.Models.ApplicationUser", b =>
                {
                    b.Navigation("ApplicationUserBooks");

                    b.Navigation("ReadLists");

                    b.Navigation("WantToReadLists");
                });

            modelBuilder.Entity("BookApp.Data.Models.Author", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("BookApp.Data.Models.Book", b =>
                {
                    b.Navigation("BookApplicationUsers");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
