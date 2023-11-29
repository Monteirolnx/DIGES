namespace Controle.Atividades.Server.Components.Pages;

public class TesteCopilot
{
    public int CalculateDaysBetweenDates(DateTime startDate, DateTime endDate)
    {
         return (endDate - startDate).Days;
     }

    public int calculaIdade(DateTime dataNascimento)
    {
        var idade = DateTime.Now.Year - dataNascimento.Year;
        if (DateTime.Now.DayOfYear < dataNascimento.DayOfYear)
            idade = idade - 1;

        return idade;
    }
}