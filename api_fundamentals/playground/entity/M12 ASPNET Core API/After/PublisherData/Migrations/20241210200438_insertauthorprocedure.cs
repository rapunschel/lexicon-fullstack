using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PublisherData.Migrations
{
    /// <inheritdoc />
    public partial class insertauthorprocedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
              @"CREATE PROCEDURE dbo.AuthorInsert
                @firstname nvarchar(100),
                @lastname nvarchar(100)
                AS
                BEGIN
                 INSERT into [Authors] (FirstName, LastName)
                 OUTPUT INSERTED.[AuthorId]
                  Values(@firstname, @lastname);
               END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE dbo.AuthorInsert");
        }
    }
}
