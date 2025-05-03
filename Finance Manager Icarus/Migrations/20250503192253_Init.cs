using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finance_Manager_Icarus.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    Usuario_Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Nome = table.Column<string>(type: "longtext", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    Senha = table.Column<string>(type: "longtext", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    data_atualizacao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario", x => x.Usuario_Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "banco",
                columns: table => new
                {
                    Banco_Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Nome = table.Column<string>(type: "longtext", nullable: false),
                    Numero = table.Column<string>(type: "longtext", nullable: false),
                    Usuario_Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    data_atualizacao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_banco", x => x.Banco_Id);
                    table.ForeignKey(
                        name: "FK_banco_usuario_Usuario_Id",
                        column: x => x.Usuario_Id,
                        principalTable: "usuario",
                        principalColumn: "Usuario_Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "categoria",
                columns: table => new
                {
                    Categoria_Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Nome = table.Column<string>(type: "longtext", nullable: false),
                    Usuario_Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    data_atualizacao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categoria", x => x.Categoria_Id);
                    table.ForeignKey(
                        name: "FK_categoria_usuario_Usuario_Id",
                        column: x => x.Usuario_Id,
                        principalTable: "usuario",
                        principalColumn: "Usuario_Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cartao",
                columns: table => new
                {
                    Cartao_Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Numero = table.Column<string>(type: "longtext", nullable: false),
                    Banco_Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    data_atualizacao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cartao", x => x.Cartao_Id);
                    table.ForeignKey(
                        name: "FK_cartao_banco_Banco_Id",
                        column: x => x.Banco_Id,
                        principalTable: "banco",
                        principalColumn: "Banco_Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "nome_movimentacao",
                columns: table => new
                {
                    Nome_movimentacao_id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Nome = table.Column<string>(type: "longtext", nullable: false),
                    Categoria_Id = table.Column<Guid>(type: "char(36)", nullable: true),
                    Usuario_Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    data_atualizacao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nome_movimentacao", x => x.Nome_movimentacao_id);
                    table.ForeignKey(
                        name: "FK_nome_movimentacao_categoria_Categoria_Id",
                        column: x => x.Categoria_Id,
                        principalTable: "categoria",
                        principalColumn: "Categoria_Id");
                    table.ForeignKey(
                        name: "FK_nome_movimentacao_usuario_Usuario_Id",
                        column: x => x.Usuario_Id,
                        principalTable: "usuario",
                        principalColumn: "Usuario_Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "movimentacao",
                columns: table => new
                {
                    Movimentacao_Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descricao = table.Column<string>(type: "longtext", nullable: true),
                    Entrada = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Cartao_Id = table.Column<Guid>(type: "char(36)", nullable: true),
                    Banco_Id = table.Column<Guid>(type: "char(36)", nullable: true),
                    NomeMovimentacao_Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    data_atualizacao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movimentacao", x => x.Movimentacao_Id);
                    table.ForeignKey(
                        name: "FK_movimentacao_banco_Banco_Id",
                        column: x => x.Banco_Id,
                        principalTable: "banco",
                        principalColumn: "Banco_Id");
                    table.ForeignKey(
                        name: "FK_movimentacao_cartao_Cartao_Id",
                        column: x => x.Cartao_Id,
                        principalTable: "cartao",
                        principalColumn: "Cartao_Id");
                    table.ForeignKey(
                        name: "FK_movimentacao_nome_movimentacao_Movimentacao_Id",
                        column: x => x.Movimentacao_Id,
                        principalTable: "nome_movimentacao",
                        principalColumn: "Nome_movimentacao_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_banco_Usuario_Id",
                table: "banco",
                column: "Usuario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_cartao_Banco_Id",
                table: "cartao",
                column: "Banco_Id");

            migrationBuilder.CreateIndex(
                name: "IX_categoria_Usuario_Id",
                table: "categoria",
                column: "Usuario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_movimentacao_Banco_Id",
                table: "movimentacao",
                column: "Banco_Id");

            migrationBuilder.CreateIndex(
                name: "IX_movimentacao_Cartao_Id",
                table: "movimentacao",
                column: "Cartao_Id");

            migrationBuilder.CreateIndex(
                name: "IX_nome_movimentacao_Categoria_Id",
                table: "nome_movimentacao",
                column: "Categoria_Id");

            migrationBuilder.CreateIndex(
                name: "IX_nome_movimentacao_Usuario_Id",
                table: "nome_movimentacao",
                column: "Usuario_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "movimentacao");

            migrationBuilder.DropTable(
                name: "cartao");

            migrationBuilder.DropTable(
                name: "nome_movimentacao");

            migrationBuilder.DropTable(
                name: "banco");

            migrationBuilder.DropTable(
                name: "categoria");

            migrationBuilder.DropTable(
                name: "usuario");
        }
    }
}
