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
                    { new Guid("0114ca8d-f2d9-45d6-95f3-0cca5981b211"), "rsmiranda@sf.prefeitura.sp.gov.br", "Rafael Miranda", 1 },
                    { new Guid("01150ec6-79e6-4255-87a1-a927dd30dcfa"), "gmarrafon@sf.prefeitura.sp.gov.br", "Glaucon Marrafon", 1 },
                    { new Guid("22e63578-fb4d-4166-83c9-c230f7c5f04c"), "lcebarreto@sf.prefeitura.sp.gov.br", "Luis Barreto", 1 },
                    { new Guid("28ffbf80-a65f-4f14-b472-f2d4703b5cec"), "avieira@sf.prefeitura.sp.gov.br", "Arthur Vieira", 1 },
                    { new Guid("80051f70-c8df-4af4-b469-010558b6c484"), "caandrade@sf.prefeitura.sp.gov.br", "Cláudio Almeida", 1 },
                    { new Guid("a5950909-66dd-4b29-958d-f89bf61e5a07"), "csmagno@sf.prefeitura.sp.gov.br", "Cláudio Magno", 1 },
                    { new Guid("ab384029-c921-41c5-ad44-dd06efa5bd98"), "lfmleitao@sf.prefeitura.sp.gov.br", "Luis Monteiro", 1 },
                    { new Guid("c8c43f6f-8787-4227-a081-edf69c5b4e49"), "askaam@sf.prefeitura.sp.gov.br", "Alex Kaam", 1 },
                    { new Guid("e491e8fa-08ab-43f3-8e5c-b634bea42427"), "fmsilva@sf.prefeitura.sp.gov.br", "Félix Silva", 1 },
                    { new Guid("f5099e10-c19f-40c7-8b1f-38ddc562b1f2"), "tsmachado@sf.prefeitura.sp.gov.br", "Thiago Maximiliano", 1 },
                    { new Guid("fa06cdd0-4e49-4d40-bcc5-fccc45bff2b3"), "gcsantanna@sf.prefeitura.sp.gov.br", "Gabriel Capetini", 1 },
                    { new Guid("fe1288c6-327a-4b36-9b54-7d801fce63af"), "rgsouza@sf.prefeitura.sp.gov.br", "Renan Guedes", 1 }
                });

            migrationBuilder.InsertData(
                table: "lider",
                columns: new[] { "codigo", "email", "nome", "ativo" },
                values: new object[,]
                {
                    { new Guid("0da8e86f-1667-49eb-bbeb-08a5ae011b83"), "rodrigomallmann@sf.prefeitura.sp.gov.br", "Rodrigo Guerra", 1 },
                    { new Guid("11e41433-bbb2-437d-8db1-fe290a71e943"), "rpioli@sf.prefeitura.sp.gov.br", "Rafael Pioli", 1 },
                    { new Guid("16c74794-4395-4661-9d37-5533d390dc43"), "fbrambilla@sf.prefeitura.sp.gov.br", "Fernando Brambilla", 1 },
                    { new Guid("9c91bd6b-3adc-4d30-88a1-f372ba7154f2"), "camendes@sf.prefeitura.sp.gov.br", "Calemino Mendes", 1 }
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
