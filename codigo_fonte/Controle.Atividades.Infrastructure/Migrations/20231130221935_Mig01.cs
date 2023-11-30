using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Controle.Atividades.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Mig01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "analista",
                columns: table => new
                {
                    codigo = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    ativo = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_analista", x => x.codigo);
                });

            migrationBuilder.CreateTable(
                name: "lider",
                columns: table => new
                {
                    codigo = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    ativo = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lider", x => x.codigo);
                });

            migrationBuilder.CreateTable(
                name: "atividade",
                columns: table => new
                {
                    cd_atividade = table.Column<Guid>(type: "uuid", nullable: false),
                    nm_redmine = table.Column<int>(type: "integer", nullable: true),
                    descricao = table.Column<string>(type: "text", nullable: false),
                    sistema = table.Column<string>(type: "text", nullable: false),
                    codigo_analista = table.Column<Guid>(type: "uuid", nullable: true),
                    codigo_lider = table.Column<Guid>(type: "uuid", nullable: true),
                    dt_criacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    dt_modificacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    dt_fechamento = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_atividade", x => x.cd_atividade);
                    table.ForeignKey(
                        name: "FK_atividade_analista_codigo_analista",
                        column: x => x.codigo_analista,
                        principalTable: "analista",
                        principalColumn: "codigo");
                    table.ForeignKey(
                        name: "FK_atividade_lider_codigo_lider",
                        column: x => x.codigo_lider,
                        principalTable: "lider",
                        principalColumn: "codigo");
                });

            migrationBuilder.CreateTable(
                name: "observacao",
                columns: table => new
                {
                    cd_observacao = table.Column<Guid>(type: "uuid", nullable: false),
                    data = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    registro = table.Column<string>(type: "text", nullable: false),
                    cd_atividade = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_observacao", x => x.cd_observacao);
                    table.ForeignKey(
                        name: "FK_observacao_atividade_cd_atividade",
                        column: x => x.cd_atividade,
                        principalTable: "atividade",
                        principalColumn: "cd_atividade",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "analista",
                columns: new[] { "codigo", "email", "nome", "ativo" },
                values: new object[,]
                {
                    { new Guid("0284a6a8-98bd-491f-8e23-6b244801f61c"), "avieira@sf.prefeitura.sp.gov.br", "Arthur Vieira", 1 },
                    { new Guid("0cd88e71-7b16-42b4-ace6-f174974c2c53"), "caandrade@sf.prefeitura.sp.gov.br", "Cláudio Almeida", 1 },
                    { new Guid("86bfbf63-8490-4489-ae6d-ecfedf7fc352"), "rsmiranda@sf.prefeitura.sp.gov.br", "Rafael Miranda", 1 },
                    { new Guid("938b084c-4d08-4ee8-8683-8235ac62aba0"), "gmarrafon@sf.prefeitura.sp.gov.br", "Glaucon Marrafon", 1 },
                    { new Guid("a57d2763-ad4e-43ef-b1ab-bf4c48bb674c"), "lfmleitao@sf.prefeitura.sp.gov.br", "Luis Monteiro", 1 },
                    { new Guid("a606e3a1-640d-482a-947c-a86cbbe9889c"), "rgsouza@sf.prefeitura.sp.gov.br", "Renan Guedes", 1 },
                    { new Guid("b65bccf4-886c-4ba6-80c5-eae51f5b02d1"), "", "Todos", 1 },
                    { new Guid("bd8e7afb-2fd1-4847-bca0-0220653c73fe"), "tsmachado@sf.prefeitura.sp.gov.br", "Thiago Maximiliano", 1 },
                    { new Guid("cbff8adc-22d0-4667-85a0-9d30369491b7"), "gcsantanna@sf.prefeitura.sp.gov.br", "Gabriel Capetini", 1 },
                    { new Guid("d3562e25-bc93-4979-8385-fa8b0b91d01c"), "csmagno@sf.prefeitura.sp.gov.br", "Cláudio Magno", 1 },
                    { new Guid("d3ee6eba-f641-48a5-8310-cf174b732fa2"), "fmsilva@sf.prefeitura.sp.gov.br", "Félix Silva", 1 },
                    { new Guid("e66c9f1e-4ed5-4b6b-a4be-1cf72cea91d2"), "lcebarreto@sf.prefeitura.sp.gov.br", "Luis Barreto", 1 },
                    { new Guid("f494b50d-5eab-4879-9906-3e8befd78672"), "askaam@sf.prefeitura.sp.gov.br", "Alex Kaam", 1 }
                });

            migrationBuilder.InsertData(
                table: "lider",
                columns: new[] { "codigo", "email", "nome", "ativo" },
                values: new object[,]
                {
                    { new Guid("1cbd6dfb-23f0-4798-91f9-e500dc271f85"), "camendes@sf.prefeitura.sp.gov.br", "Calemino Mendes", 1 },
                    { new Guid("44819af5-0c8a-4790-9a2b-c3f3e117634c"), "", "Todos", 1 },
                    { new Guid("4db04479-f856-423b-9ff8-3a46a5ce3115"), "rpioli@sf.prefeitura.sp.gov.br", "Rafael Pioli", 1 },
                    { new Guid("611f0467-8cc5-4a13-90f1-ae00c3e91e5a"), "rodrigomallmann@sf.prefeitura.sp.gov.br", "Rodrigo Guerra", 1 },
                    { new Guid("bc2cf0b8-de82-4cc1-9e12-896f2eef3ca7"), "fbrambilla@sf.prefeitura.sp.gov.br", "Fernando Brambilla", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_atividade_codigo_analista",
                table: "atividade",
                column: "codigo_analista");

            migrationBuilder.CreateIndex(
                name: "IX_atividade_codigo_lider",
                table: "atividade",
                column: "codigo_lider");

            migrationBuilder.CreateIndex(
                name: "IX_observacao_cd_atividade",
                table: "observacao",
                column: "cd_atividade");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "observacao");

            migrationBuilder.DropTable(
                name: "atividade");

            migrationBuilder.DropTable(
                name: "analista");

            migrationBuilder.DropTable(
                name: "lider");
        }
    }
}
