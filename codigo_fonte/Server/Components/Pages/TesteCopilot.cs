namespace Controle.Atividades.Server.Components.Pages;

public class TesteCopilot
{
    public int CalculateDaysBetweenDates(DateTime startDate, DateTime endDate)
    {
         return (endDate - startDate).Days;
     }
}