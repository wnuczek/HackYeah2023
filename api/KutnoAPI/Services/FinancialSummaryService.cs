using Dapper;
using KutnoAPI.Models;
using Npgsql;

namespace KutnoAPI.Services
{
    public class FinancialSummaryService
    {
        private const string INSERT_SCHOOL_HEADER = "INSERT INTO pgj.financial_summary(regon, year, yearpart, department, chapter, paragraph, plannedincome, receivables, madeincome, plannedcost, engagement, madecost)" +
                " VALUES(@Regon, @Year, @YearPart, @Department, @Chapter, @Paragraph, @PlannedIncome, @Receivables, @MadeIncome, @PlannedCost, @Engagement, @Madecost)" +
            " ON CONFLICT (regon, year, yearpart, chapter, paragraph) DO UPDATE SET department=EXCLUDED.department, plannedincome=EXCLUDED.plannedincome, receivables=EXCLUDED.receivables, madeincome=EXCLUDED.madeincome, plannedcost=EXCLUDED.plannedcost, engagement=EXCLUDED.engagement, madecost=EXCLUDED.madecost;";


        public bool InsertDefinitionData(List<FinancialSummary> defs, NpgsqlConnection connection)
        {
            //using (var tx = connection.BeginTransaction())
            //{
            foreach (var def in defs)
            {
                connection.Execute(INSERT_SCHOOL_HEADER, def);
            }
            return true;
            //}
        }
    }
}
