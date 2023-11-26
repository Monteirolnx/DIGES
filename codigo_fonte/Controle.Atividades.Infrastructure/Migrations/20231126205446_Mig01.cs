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
                    dt_modificacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    dt_fechamento = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
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
                    { new Guid("13f14f86-ee2b-4aa4-aedf-3c34daf87705"), "lfmleitao@sf.prefeitura.sp.gov.br", "Luis Monteiro", 1 },
                    { new Guid("1b5dfad3-6624-453b-a5b5-46e060e41c90"), "avieira@sf.prefeitura.sp.gov.br", "Arthur Vieira", 1 },
                    { new Guid("2d5c150d-6446-4195-a898-5b2b8e48c707"), "caandrade@sf.prefeitura.sp.gov.br", "Cláudio Almeida", 1 },
                    { new Guid("4ae9913f-ab7c-4f68-8bb1-0c4e3a478f49"), "fmsilva@sf.prefeitura.sp.gov.br", "Félix Silva", 1 },
                    { new Guid("59c92c95-45c3-4508-b131-b29e5105ceca"), "rgsouza@sf.prefeitura.sp.gov.br", "Renan Guedes", 1 },
                    { new Guid("74d372b7-a724-4154-87fa-ad7300e2450e"), "gmarrafon@sf.prefeitura.sp.gov.br", "Glaucon Marrafon", 1 },
                    { new Guid("86672458-bc3f-472d-82cb-c5fb7b5a563c"), "lcebarreto@sf.prefeitura.sp.gov.br", "Luis Barreto", 1 },
                    { new Guid("8ae0f6ba-aadf-4a3a-8229-a98db5f17b4e"), "gcsantanna@sf.prefeitura.sp.gov.br", "Gabriel Capetini", 1 },
                    { new Guid("9a49d24b-b082-407e-b099-adc4130b941c"), "csmagno@sf.prefeitura.sp.gov.br", "Cláudio Magno", 1 },
                    { new Guid("dbaee3b0-7e5d-4340-add9-26bb67235d14"), "askaam@sf.prefeitura.sp.gov.br", "Alex Kaam", 1 },
                    { new Guid("e41df73d-5dc8-429a-b630-686272b1a9ee"), "tsmachado@sf.prefeitura.sp.gov.br", "Thiago Maximiliano", 1 },
                    { new Guid("f983a40c-5e7f-40c4-b5c9-b1f898f71713"), "rsmiranda@sf.prefeitura.sp.gov.br", "Rafael Miranda", 1 }
                });

            migrationBuilder.InsertData(
                table: "lider",
                columns: new[] { "codigo", "email", "nome", "ativo" },
                values: new object[,]
                {
                    { new Guid("2bbd0e0e-5739-45cd-81f0-7978c0ed85d3"), "rpioli@sf.prefeitura.sp.gov.br", "Rafael Pioli", 1 },
                    { new Guid("3add5538-6065-465a-8bfd-9abf7f98f7a8"), "fbrambilla@sf.prefeitura.sp.gov.br", "Fernando Brambilla", 1 },
                    { new Guid("452c9ada-1116-43e0-adc5-ce93671f81e2"), "rodrigomallmann@sf.prefeitura.sp.gov.br", "Rodrigo Guerra", 1 },
                    { new Guid("bf899e46-a1de-419e-9ea2-37eabfc9f8c0"), "camendes@sf.prefeitura.sp.gov.br", "Calemino Mendes", 1 }
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
