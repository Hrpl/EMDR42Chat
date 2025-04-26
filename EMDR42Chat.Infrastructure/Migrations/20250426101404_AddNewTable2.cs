using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMDR42Chat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_therapeft_clients_models",
                table: "therapeft_clients_models");

            migrationBuilder.RenameTable(
                name: "therapeft_clients_models",
                newName: "therapeft_clients");

            migrationBuilder.AddPrimaryKey(
                name: "pk_therapeft_clients",
                table: "therapeft_clients",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_therapeft_clients",
                table: "therapeft_clients");

            migrationBuilder.RenameTable(
                name: "therapeft_clients",
                newName: "therapeft_clients_models");

            migrationBuilder.AddPrimaryKey(
                name: "pk_therapeft_clients_models",
                table: "therapeft_clients_models",
                column: "id");
        }
    }
}
