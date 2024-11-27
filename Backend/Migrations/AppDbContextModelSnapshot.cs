// Migrations/AppDbContextModelSnapshot.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Backend.Data;

[DbContext(typeof(AppDbContext))]
partial class AppDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasAnnotation("ProductVersion", "8.0.0");

        modelBuilder.Entity("Backend.Models.User", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");

                b.Property<string>("FirstName")
                    .HasColumnType("TEXT");

                b.Property<string>("LastName")
                    .HasColumnType("TEXT");

                b.HasKey("Id");

                b.ToTable("Users");
            });

        modelBuilder.Entity("Backend.Models.UserData", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");

                b.Property<DateTime>("Date")
                    .HasColumnType("TEXT");

                b.Property<int>("UserId")
                    .HasColumnType("INTEGER");

                b.Property<int>("Value")
                    .HasColumnType("INTEGER");

                b.HasKey("Id");

                b.HasIndex("UserId");

                b.ToTable("UserDatas");
            });

        modelBuilder.Entity("Backend.Models.UserData", b =>
            {
                b.HasOne("Backend.Models.User", "User")
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });
    }
}