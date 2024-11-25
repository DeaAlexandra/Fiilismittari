// Migrations/20241120105346_InitialCreate.Designer.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Backend.Data;

[DbContext(typeof(AppDbContext))]
[Migration("20241120105346_InitialCreate")]
partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                FirstName = table.Column<string>(nullable: true),
                LastName = table.Column<string>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "UserDatas",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                UserId = table.Column<int>(nullable: false),
                Date = table.Column<DateTime>(nullable: false),
                Value = table.Column<int>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserDatas", x => x.Id);
                table.ForeignKey(
                    name: "FK_UserDatas_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_UserDatas_UserId",
            table: "UserDatas",
            column: "UserId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "UserDatas");

        migrationBuilder.DropTable(
            name: "Users");
    }

    protected override void BuildTargetModel(ModelBuilder modelBuilder)
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