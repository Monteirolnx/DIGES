namespace Controle.Atividades.Client.Utils;

public static class  HtmlUtil
{
    public static string CriarAta(AtaDto ata)
    {
        var resultado = new StringBuilder();

        resultado.AppendLine("<html>");
        resultado.AppendLine("<head>");
        resultado.AppendLine("<style>");
        resultado.AppendLine("body { font-family: Calibri, sans-serif; font-size: 11pt; color: rgb(0,0,0); }");
        resultado.AppendLine("table { width: 100%; border-collapse: collapse; }");
        resultado.AppendLine("th, td { border: 1px solid black; padding: 5px; text-align: left; }");
        resultado.AppendLine("th { background-color: rgb(231,230,230); }");
        resultado.AppendLine("td { white-space: pre-wrap; }");
        resultado.AppendLine("</style>");
        resultado.AppendLine("</head>");
        resultado.AppendLine("<body>");
        resultado.AppendLine("<div dir='ltr'>");
        resultado.AppendLine($"<p>Segue ata da reunião: {DateTime.Now.ToString("dd/MM/yyyy")}.</p>");

        resultado.AppendLine($"<p><b><u>Atividades criadas:</u></b> {ata.AtividadesCriadas}</p>");
        resultado.AppendLine($"<p><b><u>Atividades atualizadas:</u></b> {ata.AtividadesAtualizadas}</p>");
        resultado.AppendLine($"<p><b><u>Atividades finalizadas:</u></b> {ata.AtividadesFinalizadas}</p>");

        resultado.AppendLine("<table>");
        resultado.AppendLine(
            "<tr><th>Atividade</th><th>Descrição</th><th>Sistema</th><th>Analista</th><th>Última observação</th></tr>");

        if (ata.AtividadesDto != null && ata.AtividadesDto.Any())
        {
            foreach (var atividadeDto in ata.AtividadesDto)
            {
                resultado.AppendLine($"<tr><td>{atividadeDto.NumeroRedmine}</td>" +
                                       $"<td>{atividadeDto.Descricao}</td>" +
                                       $"<td>{atividadeDto.Sistema}</td>" +
                                       $"<td>{atividadeDto.Analista?.Nome ?? "-"}</td>" +
                                       $"<td>{atividadeDto.Historico?.OrderByDescending(x => x.Data).Select(x => $"{x.Data:dd/MM/yyyy}\n{x.Registro}").FirstOrDefault() ?? "-"}</td>" +
                                       $"</tr>");
            }
        }

        resultado.AppendLine("</table>");
        resultado.AppendLine("</div>");
        resultado.AppendLine("</body>");
        resultado.AppendLine("</html>");

        return resultado.ToString();
    }
}