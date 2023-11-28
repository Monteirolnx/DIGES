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
                    { new Guid("0acb23ec-fd56-4136-8d9b-476a5c28c373"), "lfmleitao@sf.prefeitura.sp.gov.br", "Luis Monteiro", 1 },
                    { new Guid("0d602ec9-e54a-4e5c-a7ec-376762f82228"), "fmsilva@sf.prefeitura.sp.gov.br", "Félix Silva", 1 },
                    { new Guid("458a7f19-062a-4643-9f62-4318c1518d5c"), "rgsouza@sf.prefeitura.sp.gov.br", "Renan Guedes", 1 },
                    { new Guid("6c364e7d-e18e-438a-9ad3-c6d1b1fb8e6a"), "avieira@sf.prefeitura.sp.gov.br", "Arthur Vieira", 1 },
                    { new Guid("7aca6e61-3020-4685-827a-4d5e6639645b"), "tsmachado@sf.prefeitura.sp.gov.br", "Thiago Maximiliano", 1 },
                    { new Guid("a520c331-959d-42ad-82f9-dbb2b2c79491"), "gcsantanna@sf.prefeitura.sp.gov.br", "Gabriel Capetini", 1 },
                    { new Guid("ab9a0ac4-38bf-4e40-8e87-9e3b11037b5a"), "rsmiranda@sf.prefeitura.sp.gov.br", "Rafael Miranda", 1 },
                    { new Guid("abe5dc57-8391-4806-a4bf-486a981de0aa"), "lcebarreto@sf.prefeitura.sp.gov.br", "Luis Barreto", 1 },
                    { new Guid("d0b0fe52-b524-4cbd-987f-cf2df6ab0903"), "gmarrafon@sf.prefeitura.sp.gov.br", "Glaucon Marrafon", 1 },
                    { new Guid("d5317f45-15a5-43e4-ba99-9bf8c35ab643"), "askaam@sf.prefeitura.sp.gov.br", "Alex Kaam", 1 },
                    { new Guid("d9240918-ab52-40de-ae14-2849832f4ee2"), "caandrade@sf.prefeitura.sp.gov.br", "Cláudio Almeida", 1 },
                    { new Guid("ebe38f75-f091-49e3-9555-038a74ad9c5b"), "csmagno@sf.prefeitura.sp.gov.br", "Cláudio Magno", 1 }
                });

            migrationBuilder.InsertData(
                table: "lider",
                columns: new[] { "codigo", "email", "nome", "ativo" },
                values: new object[,]
                {
                    { new Guid("43862231-caff-4f25-955e-3bc5da729974"), "fbrambilla@sf.prefeitura.sp.gov.br", "Fernando Brambilla", 1 },
                    { new Guid("59af77c0-980e-44ba-a54e-3104f21d5efe"), "camendes@sf.prefeitura.sp.gov.br", "Calemino Mendes", 1 },
                    { new Guid("b4010015-dc2b-473d-a486-751ceecab6d4"), "rpioli@sf.prefeitura.sp.gov.br", "Rafael Pioli", 1 },
                    { new Guid("bcb63412-cdd5-4455-a465-3e6f0867dce6"), "rodrigomallmann@sf.prefeitura.sp.gov.br", "Rodrigo Guerra", 1 }
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
