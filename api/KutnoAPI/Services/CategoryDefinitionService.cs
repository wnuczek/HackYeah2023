using Dapper;
using KutnoAPI.Models;
using Npgsql;

namespace KutnoAPI.Services
{
    public class CategoryDefinitionService
    {
        private const string INSERT_SCHOOL_HEADER = "INSERT INTO pgj.category_definitions(symbol, \"year\", description, factor)" +
	        " VALUES(@Symbol, @Year, @Description, @Factor)" +
            " ON CONFLICT (symbol, \"year\") DO UPDATE SET Description=EXCLUDED.Description, Factor=EXCLUDED.Factor;";


        public bool InsertDefinitionData(List<CategoryDefinition> defs, NpgsqlConnection connection)
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
