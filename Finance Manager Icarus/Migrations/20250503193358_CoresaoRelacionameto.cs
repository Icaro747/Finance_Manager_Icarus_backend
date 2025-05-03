using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finance_Manager_Icarus.Migrations
{
    /// <inheritdoc />
    public partial class CoresaoRelacionameto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_movimentacao_nome_movimentacao_Movimentacao_Id",
                table: "movimentacao");

            migrationBuilder.CreateIndex(
                name: "IX_movimentacao_NomeMovimentacao_Id",
                table: "movimentacao",
                column: "NomeMovimentacao_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_movimentacao_nome_movimentacao_NomeMovimentacao_Id",
                table: "movimentacao",
                column: "NomeMovimentacao_Id",
                principalTable: "nome_movimentacao",
                principalColumn: "Nome_movimentacao_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_movimentacao_nome_movimentacao_NomeMovimentacao_Id",
                table: "movimentacao");

            migrationBuilder.DropIndex(
                name: "IX_movimentacao_NomeMovimentacao_Id",
                table: "movimentacao");

            migrationBuilder.AddForeignKey(
                name: "FK_movimentacao_nome_movimentacao_Movimentacao_Id",
                table: "movimentacao",
                column: "Movimentacao_Id",
                principalTable: "nome_movimentacao",
                principalColumn: "Nome_movimentacao_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
