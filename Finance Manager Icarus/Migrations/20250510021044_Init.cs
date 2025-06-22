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
                name: "Usuario",
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
                    table.PrimaryKey("PK_Usuario", x => x.Usuario_Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Banco",
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
                    table.PrimaryKey("PK_Banco", x => x.Banco_Id);
                    table.ForeignKey(
                        name: "FK_Banco_Usuario_Usuario_Id",
                        column: x => x.Usuario_Id,
                        principalTable: "Usuario",
                        principalColumn: "Usuario_Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Categoria",
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
                    table.PrimaryKey("PK_Categoria", x => x.Categoria_Id);
                    table.ForeignKey(
                        name: "FK_Categoria_Usuario_Usuario_Id",
                        column: x => x.Usuario_Id,
                        principalTable: "Usuario",
                        principalColumn: "Usuario_Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Mapeamento",
                columns: table => new
                {
                    Mapeamento_Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Nome = table.Column<string>(type: "longtext", nullable: false),
                    Usuario_Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    data_atualizacao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mapeamento", x => x.Mapeamento_Id);
                    table.ForeignKey(
                        name: "FK_Mapeamento_Usuario_Usuario_Id",
                        column: x => x.Usuario_Id,
                        principalTable: "Usuario",
                        principalColumn: "Usuario_Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tipo_Movimentacao",
                columns: table => new
                {
                    Tipo_Movimentacao_Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Nome = table.Column<string>(type: "longtext", nullable: false),
                    Usuario_Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    data_atualizacao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_Movimentacao", x => x.Tipo_Movimentacao_Id);
                    table.ForeignKey(
                        name: "FK_Tipo_Movimentacao_Usuario_Usuario_Id",
                        column: x => x.Usuario_Id,
                        principalTable: "Usuario",
                        principalColumn: "Usuario_Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cartao",
                columns: table => new
                {
                    Cartao_Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Nome = table.Column<string>(type: "longtext", nullable: false),
                    Numero = table.Column<string>(type: "longtext", nullable: false),
                    Banco_Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    data_atualizacao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cartao", x => x.Cartao_Id);
                    table.ForeignKey(
                        name: "FK_Cartao_Banco_Banco_Id",
                        column: x => x.Banco_Id,
                        principalTable: "Banco",
                        principalColumn: "Banco_Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Nome_Movimentacao",
                columns: table => new
                {
                    Nome_Movimentacao_Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Nome = table.Column<string>(type: "longtext", nullable: false),
                    Categoria_Id = table.Column<Guid>(type: "char(36)", nullable: true),
                    Usuario_Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    data_atualizacao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nome_Movimentacao", x => x.Nome_Movimentacao_Id);
                    table.ForeignKey(
                        name: "FK_Nome_Movimentacao_Categoria_Categoria_Id",
                        column: x => x.Categoria_Id,
                        principalTable: "Categoria",
                        principalColumn: "Categoria_Id");
                    table.ForeignKey(
                        name: "FK_Nome_Movimentacao_Usuario_Usuario_Id",
                        column: x => x.Usuario_Id,
                        principalTable: "Usuario",
                        principalColumn: "Usuario_Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Itens_Mapeamento",
                columns: table => new
                {
                    Itens_Mapeamento_Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Campo_Destino = table.Column<string>(type: "longtext", nullable: false),
                    Campo_Origem = table.Column<string>(type: "longtext", nullable: false),
                    Mapeamento_Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    data_atualizacao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Itens_Mapeamento", x => x.Itens_Mapeamento_Id);
                    table.ForeignKey(
                        name: "FK_Itens_Mapeamento_Mapeamento_Mapeamento_Id",
                        column: x => x.Mapeamento_Id,
                        principalTable: "Mapeamento",
                        principalColumn: "Mapeamento_Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Movimentacao",
                columns: table => new
                {
                    Movimentacao_Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descricao = table.Column<string>(type: "longtext", nullable: true),
                    Entrada = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Cartao_Id = table.Column<Guid>(type: "char(36)", nullable: true),
                    Banco_Id = table.Column<Guid>(type: "char(36)", nullable: true),
                    Nome_Movimentacao_Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Tipo_Movimentacao_Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    data_atualizacao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimentacao", x => x.Movimentacao_Id);
                    table.ForeignKey(
                        name: "FK_Movimentacao_Banco_Banco_Id",
                        column: x => x.Banco_Id,
                        principalTable: "Banco",
                        principalColumn: "Banco_Id");
                    table.ForeignKey(
                        name: "FK_Movimentacao_Cartao_Cartao_Id",
                        column: x => x.Cartao_Id,
                        principalTable: "Cartao",
                        principalColumn: "Cartao_Id");
                    table.ForeignKey(
                        name: "FK_Movimentacao_Nome_Movimentacao_Nome_Movimentacao_Id",
                        column: x => x.Nome_Movimentacao_Id,
                        principalTable: "Nome_Movimentacao",
                        principalColumn: "Nome_Movimentacao_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movimentacao_Tipo_Movimentacao_Tipo_Movimentacao_Id",
                        column: x => x.Tipo_Movimentacao_Id,
                        principalTable: "Tipo_Movimentacao",
                        principalColumn: "Tipo_Movimentacao_Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Banco_Usuario_Id",
                table: "Banco",
                column: "Usuario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Cartao_Banco_Id",
                table: "Cartao",
                column: "Banco_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Categoria_Usuario_Id",
                table: "Categoria",
                column: "Usuario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Itens_Mapeamento_Mapeamento_Id",
                table: "Itens_Mapeamento",
                column: "Mapeamento_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Mapeamento_Usuario_Id",
                table: "Mapeamento",
                column: "Usuario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacao_Banco_Id",
                table: "Movimentacao",
                column: "Banco_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacao_Cartao_Id",
                table: "Movimentacao",
                column: "Cartao_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacao_Nome_Movimentacao_Id",
                table: "Movimentacao",
                column: "Nome_Movimentacao_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacao_Tipo_Movimentacao_Id",
                table: "Movimentacao",
                column: "Tipo_Movimentacao_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Nome_Movimentacao_Categoria_Id",
                table: "Nome_Movimentacao",
                column: "Categoria_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Nome_Movimentacao_Usuario_Id",
                table: "Nome_Movimentacao",
                column: "Usuario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tipo_Movimentacao_Usuario_Id",
                table: "Tipo_Movimentacao",
                column: "Usuario_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Itens_Mapeamento");

            migrationBuilder.DropTable(
                name: "Movimentacao");

            migrationBuilder.DropTable(
                name: "Mapeamento");

            migrationBuilder.DropTable(
                name: "Cartao");

            migrationBuilder.DropTable(
                name: "Nome_Movimentacao");

            migrationBuilder.DropTable(
                name: "Tipo_Movimentacao");

            migrationBuilder.DropTable(
                name: "Banco");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
