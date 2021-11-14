﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.DAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Picture = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    About = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    YoutubeLink = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TwitterLink = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    InstagramLink = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    FacebookLink = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    LinkedInLink = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    GitHubLink = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    WebsiteLink = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Content = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    Thumbnail = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ViewsCount = table.Column<int>(type: "int", nullable: false),
                    CommentCount = table.Column<int>(type: "int", nullable: false),
                    SeoAuthor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SeoDescription = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SeoTags = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Articles_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Articles_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ArticleID = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Comments_Articles_ArticleID",
                        column: x => x.ArticleID,
                        principalTable: "Articles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "ID", "CreatedByName", "CreatedDate", "Description", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Name", "Note" },
                values: new object[,]
                {
                    { 1, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 722, DateTimeKind.Local).AddTicks(1345), "Kediler ile İlgili En Güncel Bilgiler", true, false, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 722, DateTimeKind.Local).AddTicks(1368), "Kedi", "Kedi Blog Kategorisi" },
                    { 2, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 722, DateTimeKind.Local).AddTicks(1393), "Köpekler ile İlgili En Güncel Bilgiler", true, false, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 722, DateTimeKind.Local).AddTicks(1396), "Köpek", "Köpek Blog Kategorisi" },
                    { 3, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 722, DateTimeKind.Local).AddTicks(1404), "Hamsterlar ile İlgili En Güncel Bilgiler", true, false, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 722, DateTimeKind.Local).AddTicks(1406), "Hamster", "Hamster Blog Kategorisi" },
                    { 4, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 722, DateTimeKind.Local).AddTicks(1414), "Kuşlar ile İlgili En Güncel Bilgiler", true, false, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 722, DateTimeKind.Local).AddTicks(1416), "Kuş", "Kuş Blog Kategorisi" },
                    { 5, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 722, DateTimeKind.Local).AddTicks(1424), "Tavşanlar ile İlgili En Güncel Bilgiler", true, false, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 722, DateTimeKind.Local).AddTicks(1426), "Tavşan", "Tavşan Blog Kategorisi" },
                    { 6, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 722, DateTimeKind.Local).AddTicks(1433), "Yılan ile İlgili En Güncel Bilgiler", true, false, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 722, DateTimeKind.Local).AddTicks(1436), "Yılan", "Yılan Blog Kategorisi" },
                    { 7, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 722, DateTimeKind.Local).AddTicks(1443), "Balıklar ile İlgili En Güncel Bilgiler", true, false, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 722, DateTimeKind.Local).AddTicks(1445), "Balık", "Balık Blog Kategorisi" },
                    { 8, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 722, DateTimeKind.Local).AddTicks(1452), "Civcivler ile İlgili En Güncel Bilgiler", true, false, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 722, DateTimeKind.Local).AddTicks(1455), "Civciv", "Civciv Blog Kategorisi" },
                    { 9, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 722, DateTimeKind.Local).AddTicks(1462), "İnekler ile İlgili En Güncel Bilgiler", true, false, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 722, DateTimeKind.Local).AddTicks(1464), "İnek", "İnek Blog Kategorisi" },
                    { 10, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 722, DateTimeKind.Local).AddTicks(1472), "Develer ile İlgili En Güncel Bilgiler", true, false, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 722, DateTimeKind.Local).AddTicks(1474), "Deve", "Deve Blog Kategorisi" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 14, "be4a3825-765d-48e1-bfad-b03c31ab9670", "Role.Read", "ROLE.READ" },
                    { 15, "4335fe1b-a19f-4f24-901a-6490efaee557", "Role.Update", "ROLE.UPDATE" },
                    { 16, "c274ab35-e4a1-41d7-bd7a-b97af5c5b8d8", "Role.Delete", "ROLE.DELETE" },
                    { 17, "77b60783-976f-488b-a1ee-30699d9b1e2d", "Comment.Create", "COMMENT.CREATE" },
                    { 22, "6343e8fc-a105-4e69-8f7d-3029e9771b6c", "SuperAdmin", "SUPERADMIN" },
                    { 19, "1309e5be-cd05-44a0-ab94-44c978c8136f", "Comment.Update", "COMMENT.UPDATE" },
                    { 20, "44aa1a97-85b7-4b28-8b27-31cb4864f9c1", "Comment.Delete", "COMMENT.DELETE" },
                    { 21, "6edc1729-d238-4ce9-a95e-7db8b47c9896", "AdminArea.Home.Read", "ADMINAREA.HOME.READ" },
                    { 13, "d90e438e-4310-4c7b-8889-b68afbc3f5b1", "Role.Create", "ROLE.CREATE" },
                    { 18, "58cb83e6-47ed-4dc0-bdb2-b4c6b54bdab0", "Comment.Read", "COMMENT.READ" },
                    { 12, "fba08076-84e5-4398-a058-7cdd9c097d99", "User.Delete", "USER.DELETE" },
                    { 7, "c1bd87a3-b120-4adf-ae6a-4b5125b5b800", "Article.Update", "ARTICLE.UPDATE" },
                    { 10, "07d8abf4-ba8f-4955-800a-d02aafa51795", "User.Read", "USER.READ" },
                    { 9, "b5a5e694-bcd5-4672-a95e-b58e8f56c1ef", "User.Create", "USER.CREATE" },
                    { 8, "ab473fad-21fe-4703-ab95-200a03a06b38", "Article.Delete", "ARTICLE.DELETE" },
                    { 6, "184da774-4320-4824-90e9-d112ded93ed9", "Article.Read", "ARTICLE.READ" },
                    { 5, "9b2af5e3-de1b-49eb-8975-3644b56b497d", "Article.Create", "ARTICLE.CREATE" },
                    { 4, "86b49c4d-4fa9-44b2-bc67-bbc4ad1a3b62", "Category.Delete", "CATEGORY.DELETE" },
                    { 3, "f4246ecf-feb1-4986-b3a6-f4cc5981f62e", "Category.Update", "CATEGORY.UPDATE" },
                    { 2, "77fccbf6-2a37-48be-b431-80063d29a1a1", "Category.Read", "CATEGORY.READ" },
                    { 1, "08ecea26-a5d7-4785-bb4d-683db9c2c2b6", "Category.Create", "CATEGORY.CREATE" },
                    { 11, "ce0d2bf0-a205-42cc-83c6-06b3f8556ea5", "User.Update", "USER.UPDATE" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "About", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FacebookLink", "FirstName", "GitHubLink", "InstagramLink", "LastName", "LinkedInLink", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Picture", "SecurityStamp", "TwitterLink", "TwoFactorEnabled", "UserName", "WebsiteLink", "YoutubeLink" },
                values: new object[,]
                {
                    { 1, "Admin User of ProgrammersBlog", 0, "3e2ccf00-d613-4f1e-a9b3-aa32f0e70272", "adminuser@gmail.com", true, "https://facebook.com/adminuser", "Admin", "https://github.com/adminuser", "https://instagram.com/adminuser", "User", "https://linkedin.com/adminuser", false, null, "ADMINUSER@GMAIL.COM", "ADMINUSER", "AQAAAAEAACcQAAAAEKAgnHH1K0EaqkHvjGOyOc3ISy61VAnxeLNdgTiFO1+iGFMe04pgVRKyPGhL/b2Ecg==", "+905555555555", true, "/userImages/defaultUser.png", "407e4c70-0310-4e98-831e-1898f66bc0c7", "https://twitter.com/adminuser", false, "adminuser", "https://programmersblog.com/", "https://youtube.com/adminuser" },
                    { 2, "Editor User of ProgrammersBlog", 0, "406757df-76d7-42a1-bf55-671cdf9912d1", "editoruser@gmail.com", true, "https://facebook.com/editoruser", "Admin", "https://github.com/editoruser", "https://instagram.com/editoruser", "User", "https://linkedin.com/editoruser", false, null, "EDITORUSER@GMAIL.COM", "EDITORUSER", "AQAAAAEAACcQAAAAELqLivfheGHdlOqfoXxz9Paha6CRdBxkfPzTYb0//UBtkWwS4FgXJaPcoT+LRvoTkQ==", "+905555555555", true, "/userImages/defaultUser.png", "d8d60a77-4d58-4fe0-8817-69d9c928e51c", "https://twitter.com/editoruser", false, "editoruser", "https://programmersblog.com/", "https://youtube.com/editoruser" }
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "ID", "CategoryID", "CommentCount", "Content", "CreatedByName", "CreatedDate", "Date", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "SeoAuthor", "SeoDescription", "SeoTags", "Thumbnail", "Title", "UserID", "ViewsCount" },
                values: new object[,]
                {
                    { 1, 1, 0, "Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir. Lorem Ipsum, adı bilinmeyen bir matbaacının bir hurufat numune kitabı oluşturmak üzere bir yazı galerisini alarak karıştırdığı 1500'lerden beri endüstri standardı sahte metinler olarak kullanılmıştır. Beşyüz yıl boyunca varlığını sürdürmekle kalmamış, aynı zamanda pek değişmeden elektronik dizgiye de sıçramıştır. 1960'larda Lorem Ipsum pasajları da içeren Letraset yapraklarının yayınlanması ile ve yakın zamanda Aldus PageMaker gibi Lorem Ipsum sürümleri içeren masaüstü yayıncılık yazılımları ile popüler olmuştur.", "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 714, DateTimeKind.Local).AddTicks(4500), new DateTime(2021, 11, 14, 18, 47, 13, 714, DateTimeKind.Local).AddTicks(659), true, false, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 714, DateTimeKind.Local).AddTicks(6474), "Kedi Nezlesi", "Ceren Oztunc", "Kedi Nezlesi", "Kedi, Kediler, Pembe, Pati, Nezle", "postImages/bremen.jpg", "Pembe Burun", 1, 100 },
                    { 10, 10, 0, "Esistono innumerevoli variazioni dei passaggi del Lorem Ipsum, ma la maggior parte hanno subito delle variazioni del tempo, a causa dell’inserimento di passaggi ironici, o di sequenze casuali di caratteri palesemente poco verosimili. Se si decide di utilizzare un passaggio del Lorem Ipsum, è bene essere certi che non contenga nulla di imbarazzante. In genere, i generatori di testo segnaposto disponibili su internet tendono a ripetere paragrafi predefiniti, rendendo questo il primo vero generatore automatico su intenet. Infatti utilizza un dizionario di oltre 200 vocaboli latini, combinati con un insieme di modelli di strutture di periodi, per generare passaggi di testo verosimili. Il testo così generato è sempre privo di ripetizioni, parole imbarazzanti o fuori luogo ecc.", "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 715, DateTimeKind.Local).AddTicks(1287), new DateTime(2021, 11, 14, 18, 47, 13, 715, DateTimeKind.Local).AddTicks(1284), true, false, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 715, DateTimeKind.Local).AddTicks(1290), "Deve", "Ceren Oztunc", "Develere Binmek", "Deve", "postImages/bremen.jpg", "Develer", 1, 26777 },
                    { 8, 8, 0, "Plusieurs variations de Lorem Ipsum peuvent être trouvées ici ou là, mais la majeure partie d'entre elles a été altérée par l'addition d'humour ou de mots aléatoires qui ne ressemblent pas une seconde à du texte standard. Si vous voulez utiliser un passage du Lorem Ipsum, vous devez être sûr qu'il n'y a rien d'embarrassant caché dans le texte. Tous les générateurs de Lorem Ipsum sur Internet tendent à reproduire le même extrait sans fin, ce qui fait de lipsum.com le seul vrai générateur de Lorem Ipsum. Iil utilise un dictionnaire de plus de 200 mots latins, en combinaison de plusieurs structures de phrases, pour générer un Lorem Ipsum irréprochable. Le Lorem Ipsum ainsi obtenu ne contient aucune répétition, ni ne contient des mots farfelus, ou des touches d'humour.", "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 715, DateTimeKind.Local).AddTicks(1259), new DateTime(2021, 11, 14, 18, 47, 13, 715, DateTimeKind.Local).AddTicks(1256), true, false, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 715, DateTimeKind.Local).AddTicks(1262), "Civcivler ne yer?", "Ceren Oztunc", "Civcivler ne yer?", "Civciv, yemek, tahıl", "postImages/bremen.jpg", "Cikcik Cicişler", 1, 750 },
                    { 7, 7, 0, "Contrairement à une opinion répandue, le Lorem Ipsum n'est pas simplement du texte aléatoire. Il trouve ses racines dans une oeuvre de la littérature latine classique datant de 45 av. J.-C., le rendant vieux de 2000 ans. Un professeur du Hampden-Sydney College, en Virginie, s'est intéressé à un des mots latins les plus obscurs, consectetur, extrait d'un passage du Lorem Ipsum, et en étudiant tous les usages de ce mot dans la littérature classique, découvrit la source incontestable du Lorem Ipsum. Il provient en fait des sections 1.10.32 et 1.10.33 du 0De Finibus Bonorum et Malorum' (Des Suprêmes Biens et des Suprêmes Maux) de Cicéron. Cet ouvrage, très populaire pendant la Renaissance, est un traité sur la théorie de l'éthique. Les premières lignes du Lorem Ipsum, 'Lorem ipsum dolor sit amet...'', proviennent de la section 1.10.32", "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 715, DateTimeKind.Local).AddTicks(1245), new DateTime(2021, 11, 14, 18, 47, 13, 715, DateTimeKind.Local).AddTicks(1241), true, false, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 715, DateTimeKind.Local).AddTicks(1248), "Balıklar onları koyduğumuz kavanozlar yerine denizde mi olmak isterdi?", "Ceren Oztunc", "Balıklar onları koyduğumuz kavanozlar yerine denizde mi olmak isterdi?", "Balık, Kova, Deniz", "postImages/bremen.jpg", "Kovada Balık", 1, 4818 },
                    { 6, 6, 0, "Le Lorem Ipsum est simplement du faux texte employé dans la composition et la mise en page avant impression. Le Lorem Ipsum est le faux texte standard de l'imprimerie depuis les années 1500, quand un imprimeur anonyme assembla ensemble des morceaux de texte pour réaliser un livre spécimen de polices de texte. Il n'a pas fait que survivre cinq siècles, mais s'est aussi adapté à la bureautique informatique, sans que son contenu n'en soit modifié. Il a été popularisé dans les années 1960 grâce à la vente de feuilles Letraset contenant des passages du Lorem Ipsum, et, plus récemment, par son inclusion dans des applications de mise en page de texte, comme Aldus PageMaker.", "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 715, DateTimeKind.Local).AddTicks(1229), new DateTime(2021, 11, 14, 18, 47, 13, 715, DateTimeKind.Local).AddTicks(1225), true, false, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 715, DateTimeKind.Local).AddTicks(1231), "Size sarılıyorsa sizi yemeyi planlar..", "Ceren Oztunc", "Yılan Tıslaması", "Yılan, Tısss, Aç kalırsa seni yer mi?", "postImages/bremen.jpg", "Tıssssss", 1, 9999 },
                    { 9, 9, 0, "Al contrario di quanto si pensi, Lorem Ipsum non è semplicemente una sequenza casuale di caratteri. Risale ad un classico della letteratura latina del 45 AC, cosa che lo rende vecchio di 2000 anni. Richard McClintock, professore di latino al Hampden-Sydney College in Virginia, ha ricercato una delle più oscure parole latine, consectetur, da un passaggio del Lorem Ipsum e ha scoperto tra i vari testi in cui è citata, la fonte da cui è tratto il testo, le sezioni 1.10.32 and 1.10.33 del 'de Finibus Bonorum et Malorum' di Cicerone. Questo testo è un trattato su teorie di etica, molto popolare nel Rinascimento. La prima riga del Lorem Ipsum, 'Lorem ipsum dolor sit amet..'', è tratta da un passaggio della sezione 1.10.32.", "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 715, DateTimeKind.Local).AddTicks(1273), new DateTime(2021, 11, 14, 18, 47, 13, 715, DateTimeKind.Local).AddTicks(1270), true, false, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 715, DateTimeKind.Local).AddTicks(1275), "İneklere Mozart Dinletmek Sütlerini Çoğaltır", "Ceren Oztunc", "İneklere Mozart Dinletmek", "inek, süt, mozart", "postImages/bremen.jpg", "Möööööööö", 1, 14900 },
                    { 4, 4, 0, "É um facto estabelecido de que um leitor é distraído pelo conteúdo legível de uma página quando analisa a sua mancha gráfica. Logo, o uso de Lorem Ipsum leva a uma distribuição mais ou menos normal de letras, ao contrário do uso de 'Conteúdo aqui,conteúdo aqui'', tornando-o texto legível. Muitas ferramentas de publicação electrónica e editores de páginas web usam actualmente o Lorem Ipsum como o modelo de texto usado por omissão, e uma pesquisa por 'lorem ipsum' irá encontrar muitos websites ainda na sua infância. Várias versões têm evoluído ao longo dos anos, por vezes por acidente, por vezes propositadamente (como no caso do humor).", "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 715, DateTimeKind.Local).AddTicks(1199), new DateTime(2021, 11, 14, 18, 47, 13, 715, DateTimeKind.Local).AddTicks(1196), true, false, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 715, DateTimeKind.Local).AddTicks(1201), "Kuşların ötüşü", "Ceren Oztunc", "Kuşların ötüşü", "Kuş, Gaga, Ötüş", "postImages/bremen.jpg", "Kuş Gagası", 1, 666 },
                    { 3, 3, 0, "Yaygın inancın tersine, Lorem Ipsum rastgele sözcüklerden oluşmaz. Kökleri M.Ö. 45 tarihinden bu yana klasik Latin edebiyatına kadar uzanan 2000 yıllık bir geçmişi vardır. Virginia'daki Hampden-Sydney College'dan Latince profesörü Richard McClintock, bir Lorem Ipsum pasajında geçen ve anlaşılması en güç sözcüklerden biri olan 'consectetur' sözcüğünün klasik edebiyattaki örneklerini incelediğinde kesin bir kaynağa ulaşmıştır. Lorm Ipsum, Çiçero tarafından M.Ö. 45 tarihinde kaleme alınan \"de Finibus Bonorum et Malorum\" (İyi ve Kötünün Uç Sınırları) eserinin 1.10.32 ve 1.10.33 sayılı bölümlerinden gelmektedir. Bu kitap, ahlak kuramı üzerine bir tezdir ve Rönesans döneminde çok popüler olmuştur. Lorem Ipsum pasajının ilk satırı olan \"Lorem ipsum dolor sit amet\" 1.10.32 sayılı bölümdeki bir satırdan gelmektedir. 1500'lerden beri kullanılmakta olan standard Lorem Ipsum metinleri ilgilenenler için yeniden üretilmiştir. Çiçero tarafından yazılan 1.10.32 ve 1.10.33 bölümleri de 1914 H. Rackham çevirisinden alınan İngilizce sürümleri eşliğinde özgün biçiminden yeniden üretilmiştir.", "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 715, DateTimeKind.Local).AddTicks(1184), new DateTime(2021, 11, 14, 18, 47, 13, 715, DateTimeKind.Local).AddTicks(1181), true, false, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 715, DateTimeKind.Local).AddTicks(1187), "Hamster Kemirmesi", "Ceren Oztunc", "Hamster Kemirmesi", "Hamster, Kemirgen", "postImages/bremen.jpg", "Kesme Şeker", 1, 12 },
                    { 2, 2, 0, "Yinelenen bir sayfa içeriğinin okuyucunun dikkatini dağıttığı bilinen bir gerçektir. Lorem Ipsum kullanmanın amacı, sürekli 'buraya metin gelecek, buraya metin gelecek' yazmaya kıyasla daha dengeli bir harf dağılımı sağlayarak okunurluğu artırmasıdır. Şu anda birçok masaüstü yayıncılık paketi ve web sayfa düzenleyicisi, varsayılan mıgır metinler olarak Lorem Ipsum kullanmaktadır. Ayrıca arama motorlarında 'lorem ipsum' anahtar sözcükleri ile arama yapıldığında henüz tasarım aşamasında olan çok sayıda site listelenir. Yıllar içinde, bazen kazara, bazen bilinçli olarak (örneğin mizah katılarak), çeşitli sürümleri geliştirilmiştir.", "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 715, DateTimeKind.Local).AddTicks(1168), new DateTime(2021, 11, 14, 18, 47, 13, 715, DateTimeKind.Local).AddTicks(1163), true, false, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 715, DateTimeKind.Local).AddTicks(1171), "Köpek Havlaması", "Ceren Oztunc", "Köpek Havlaması", "Köpek, Sadık, Dost", "postImages/bremen.jpg", "Sadık Dost", 1, 295 },
                    { 5, 5, 0, "Yaygın inancın tersine, Lorem Ipsum rastgele sözcüklerden oluşmaz. Kökleri M.Ö. 45 tarihinden bu yana klasik Latin edebiyatına kadar uzanan 2000 yıllık bir geçmişi vardır. Virginia'daki Hampden-Sydney College'dan Latince profesörü Richard McClintock, bir Lorem Ipsum pasajında geçen ve anlaşılması en güç sözcüklerden biri olan 'consectetur' sözcüğünün klasik edebiyattaki örneklerini incelediğinde kesin bir kaynağa ulaşmıştır. Lorm Ipsum, Çiçero tarafından M.Ö. 45 tarihinde kaleme alınan \"de Finibus Bonorum et Malorum\" (İyi ve Kötünün Uç Sınırları) eserinin 1.10.32 ve 1.10.33 sayılı bölümlerinden gelmektedir. Bu kitap, ahlak kuramı üzerine bir tezdir ve Rönesans döneminde çok popüler olmuştur. Lorem Ipsum pasajının ilk satırı olan \"Lorem ipsum dolor sit amet\" 1.10.32 sayılı bölümdeki bir satırdan gelmektedir. 1500'lerden beri kullanılmakta olan standard Lorem Ipsum metinleri ilgilenenler için yeniden üretilmiştir. Çiçero tarafından yazılan 1.10.32 ve 1.10.33 bölümleri de 1914 H. Rackham çevirisinden alınan İngilizce sürümleri eşliğinde özgün biçiminden yeniden üretilmiştir.", "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 715, DateTimeKind.Local).AddTicks(1213), new DateTime(2021, 11, 14, 18, 47, 13, 715, DateTimeKind.Local).AddTicks(1210), true, false, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 715, DateTimeKind.Local).AddTicks(1216), "Tavşanların havuç yemesi", "Ceren Oztunc", "Tavşanların havuç yemesi", "Tavşan, Havuç, Kemirgen", "postImages/bremen.jpg", "Havuç", 1, 3225 }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 3, 2 },
                    { 20, 1 },
                    { 21, 1 },
                    { 22, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 4, 2 },
                    { 17, 2 },
                    { 6, 2 },
                    { 7, 2 },
                    { 8, 2 },
                    { 19, 1 },
                    { 18, 2 },
                    { 19, 2 },
                    { 5, 2 },
                    { 18, 1 },
                    { 13, 1 },
                    { 16, 1 },
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 1 },
                    { 5, 1 },
                    { 6, 1 },
                    { 7, 1 },
                    { 8, 1 },
                    { 9, 1 },
                    { 10, 1 },
                    { 11, 1 },
                    { 12, 1 },
                    { 20, 2 },
                    { 14, 1 }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 15, 1 });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 17, 1 });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 21, 2 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "ID", "ArticleID", "CreatedByName", "CreatedDate", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "Text" },
                values: new object[,]
                {
                    { 1, 1, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 727, DateTimeKind.Local).AddTicks(3080), true, false, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 727, DateTimeKind.Local).AddTicks(3100), "C# Makale Yorumu", "Lorem Ipsum pasajlarının birçok çeşitlemesi vardır. Ancak bunların büyük bir çoğunluğu mizah katılarak veya rastgele sözcükler eklenerek değiştirilmişlerdir. Eğer bir Lorem Ipsum pasajı kullanacaksanız, metin aralarına utandırıcı sözcükler gizlenmediğinden emin olmanız gerekir. İnternet'teki tüm Lorem Ipsum üreteçleri önceden belirlenmiş metin bloklarını yineler. Bu da, bu üreteci İnternet üzerindeki gerçek Lorem Ipsum üreteci yapar. Bu üreteç, 200'den fazla Latince sözcük ve onlara ait cümle yapılarını içeren bir sözlük kullanır. Bu nedenle, üretilen Lorem Ipsum metinleri yinelemelerden, mizahtan ve karakteristik olmayan sözcüklerden uzaktır." },
                    { 2, 2, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 727, DateTimeKind.Local).AddTicks(3124), true, false, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 727, DateTimeKind.Local).AddTicks(3126), "C++ Makale Yorumu", "Lorem Ipsum jest tekstem stosowanym jako przykładowy wypełniacz w przemyśle poligraficznym. Został po raz pierwszy użyty w XV w. przez nieznanego drukarza do wypełnienia tekstem próbnej książki. Pięć wieków później zaczął być używany przemyśle elektronicznym, pozostając praktycznie niezmienionym. Spopularyzował się w latach 60. XX w. wraz z publikacją arkuszy Letrasetu, zawierających fragmenty Lorem Ipsum, a ostatnio z zawierającym różne wersje Lorem Ipsum oprogramowaniem przeznaczonym do realizacji druków na komputerach osobistych, jak Aldus PageMaker" },
                    { 3, 3, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 727, DateTimeKind.Local).AddTicks(3133), true, false, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 727, DateTimeKind.Local).AddTicks(3135), "JavaScript Makale Yorumu", "Ang Lorem Ipsum ay ginagamit na modelo ng industriya ng pagpriprint at pagtytypeset. Ang Lorem Ipsum ang naging regular na modelo simula pa noong 1500s, noong may isang di kilalang manlilimbag and kumuha ng galley ng type at ginulo ang pagkaka-ayos nito upang makagawa ng libro ng mga type specimen. Nalagpasan nito hindi lang limang siglo, kundi nalagpasan din nito ang paglaganap ng electronic typesetting at nanatiling parehas. Sumikat ito noong 1960s kasabay ng pag labas ng Letraset sheets na mayroong mga talata ng Lorem Ipsum, at kamakailan lang sa mga desktop publishing software tulad ng Aldus Pagemaker ginamit ang mga bersyon ng Lorem Ipsum." },
                    { 4, 4, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 727, DateTimeKind.Local).AddTicks(3142), true, false, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 727, DateTimeKind.Local).AddTicks(3144), "Typescript Makale Yorumu", "Lorem Ipsum er rett og slett dummytekst fra og for trykkeindustrien. Lorem Ipsum har vært bransjens standard for dummytekst helt siden 1500-tallet, da en ukjent boktrykker stokket en mengde bokstaver for å lage et prøveeksemplar av en bok. Lorem Ipsum har tålt tidens tann usedvanlig godt, og har i tillegg til å bestå gjennom fem århundrer også tålt spranget over til elektronisk typografi uten vesentlige endringer. Lorem Ipsum ble gjort allment kjent i 1960-årene ved lanseringen av Letraset-ark med avsnitt fra Lorem Ipsum, og senere med sideombrekkingsprogrammet Aldus PageMaker som tok i bruk nettopp Lorem Ipsum for dummytekst." },
                    { 5, 5, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 727, DateTimeKind.Local).AddTicks(3151), true, false, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 727, DateTimeKind.Local).AddTicks(3153), "Java Makale Yorumu", "Lorem Ipsum este pur şi simplu o machetă pentru text a industriei tipografice. Lorem Ipsum a fost macheta standard a industriei încă din secolul al XVI-lea, când un tipograf anonim a luat o planşetă de litere şi le-a amestecat pentru a crea o carte demonstrativă pentru literele respective. Nu doar că a supravieţuit timp de cinci secole, dar şi a facut saltul în tipografia electronică practic neschimbată. A fost popularizată în anii '60 odată cu ieşirea colilor Letraset care conţineau pasaje Lorem Ipsum, iar mai recent, prin programele de publicare pentru calculator, ca Aldus PageMaker care includeau versiuni de Lorem Ipsum." },
                    { 6, 6, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 727, DateTimeKind.Local).AddTicks(3159), true, false, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 727, DateTimeKind.Local).AddTicks(3162), "Python Makale Yorumu", "Lorem Ipsum je jednostavno probni tekst koji se koristi u tiskarskoj i slovoslagarskoj industriji. Lorem Ipsum postoji kao industrijski standard još od 16-og stoljeća, kada je nepoznati tiskar uzeo tiskarsku galiju slova i posložio ih da bi napravio knjigu s uzorkom tiska. Taj je tekst ne samo preživio pet stoljeća, već se i vinuo u svijet elektronskog slovoslagarstva, ostajući u suštini nepromijenjen. Postao je popularan tijekom 1960-ih s pojavom Letraset listova s odlomcima Lorem Ipsum-a, a u skorije vrijeme sa software-om za stolno izdavaštvo kao što je Aldus PageMaker koji također sadrži varijante Lorem Ipsum-a." },
                    { 7, 7, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 727, DateTimeKind.Local).AddTicks(3168), true, false, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 727, DateTimeKind.Local).AddTicks(3171), "Php Makale Yorumu", "Lorem Ipsum – tas ir teksta salikums, kuru izmanto poligrāfijā un maketēšanas darbos. Lorem Ipsum ir kļuvis par vispārpieņemtu teksta aizvietotāju kopš 16. gadsimta sākuma. Tajā laikā kāds nezināms iespiedējs izveidoja teksta fragmentu, lai nodrukātu grāmatu ar burtu paraugiem. Tas ir ne tikai pārdzīvojis piecus gadsimtus, bet bez ievērojamām izmaiņām saglabājies arī mūsdienās, pārejot uz datorizētu teksta apstrādi. Tā popularizēšanai 60-tajos gados kalpoja Letraset burtu paraugu publicēšana ar Lorem Ipsum teksta fragmentiem un, nesenā pagātnē, tādas maketēšanas programmas kā Aldus PageMaker, kuras šablonu paraugos ir izmantots Lorem Ipsum teksts." },
                    { 8, 8, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 727, DateTimeKind.Local).AddTicks(3177), true, false, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 727, DateTimeKind.Local).AddTicks(3179), "Kotlin Makale Yorumu", "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like)." },
                    { 9, 9, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 727, DateTimeKind.Local).AddTicks(3186), true, false, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 727, DateTimeKind.Local).AddTicks(3188), "Swift Makale Yorumu", "هنالك العديد من الأنواع المتوفرة لنصوص لوريم إيبسوم، ولكن الغالبية تم تعديلها بشكل ما عبر إدخال بعض النوادر أو الكلمات العشوائية إلى النص. إن كنت تريد أن تستخدم نص لوريم إيبسوم ما، عليك أن تتحقق أولاً أن ليس هناك أي كلمات أو عبارات محرجة أو غير لائقة مخبأة في هذا النص. بينما تعمل جميع مولّدات نصوص لوريم إيبسوم على الإنترنت على إعادة تكرار مقاطع من نص لوريم إيبسوم نفسه عدة مرات بما تتطلبه الحاجة، يقوم مولّدنا هذا باستخدام كلمات من قاموس يحوي على أكثر من 200 كلمة لا تينية، مضاف إليها مجموعة من الجمل النموذجية، لتكوين نص لوريم إيبسوم ذو شكل منطقي قريب إلى النص الحقيقي. وبالتالي يكون النص الناتح خالي من التكرار، أو أي كلمات أو عبارات غير لائقة أو ما شابه. وهذا ما يجعله أول مولّد نص لوريم إيبسوم حقيقي على الإنترنت." },
                    { 10, 10, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 727, DateTimeKind.Local).AddTicks(3195), true, false, "InitialCreate", new DateTime(2021, 11, 14, 18, 47, 13, 727, DateTimeKind.Local).AddTicks(3197), "Ruby Makale Yorumu", "Lorem Ipsum，也称乱数假文或者哑元文本， 是印刷及排版领域所常用的虚拟文字。由于曾经一台匿名的打印机刻意打乱了一盒印刷字体从而造出一本字体样品书，Lorem Ipsum从西元15世纪起就被作为此领域的标准文本使用。它不仅延续了五个世纪，还通过了电子排版的挑战，其雏形却依然保存至今。在1960年代，”Leatraset”公司发布了印刷着Lorem Ipsum段落的纸张，从而广泛普及了它的使用。最近，计算机桌面出版软件”Aldus PageMaker”也通过同样的方式使Lorem Ipsum落入大众的视野。" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_CategoryID",
                table: "Articles",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_UserID",
                table: "Articles",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ArticleID",
                table: "Comments",
                column: "ArticleID");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
