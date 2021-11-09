using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.DAL.Migrations
{
    public partial class SeedingCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [RinaBlogDB].dbo.Categories (Name, Description,Note,CreatedDate,CreatedByName, ModifiedDate,ModifiedByName, IsActive,IsDeleted) VALUES ('Kediler', 'Kediler İle İlgili Güncel Bilgiler', 'Kedi Kategorisi', GETDATE(),'Migration',GETDATE(),'Migration', 1,0)"); // Bu şekilde sql sorguları gönderebiliriz burda..
            migrationBuilder.Sql("INSERT INTO [RinaBlogDB].dbo.Categories (Name, Description,Note,CreatedDate,CreatedByName, ModifiedDate,ModifiedByName, IsActive,IsDeleted) VALUES ('Köpekler', 'Köpekler İle İlgili Güncel Bilgiler', 'Köpek Kategorisi', GETDATE(),'Migration',GETDATE(),'Migration', 1,0)");
            migrationBuilder.Sql("INSERT INTO [RinaBlogDB].dbo.Categories (Name, Description,Note,CreatedDate,CreatedByName, ModifiedDate,ModifiedByName, IsActive,IsDeleted) VALUES ('Kuşlar', 'Kuşlar İle İlgili Güncel Bilgiler', 'Kuş Kategorisi', GETDATE(),'Migration',GETDATE(),'Migration', 1,0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
