using KutnoAPI.Models;
using System.Data;
using System.Xml.Linq;

namespace KutnoAPI.Parsers;

public class SchoolParser
{
	private readonly XMLParser xMLParser;

	public SchoolParser()
	{
		xMLParser = new XMLParser();
	}

	public void Parse(byte[] bytes)
	{
		string fileName = "C:\\Users\\ANI\\source\\repos\\HackYeah\\docs\\SIO 30.09.2022.xml";
		string sheetName = "Szkoły i placówki";

		var schools = xMLParser.ParseXmlToDataTable(fileName, sheetName);

		// Delete the first 5 rows from the DataTable
		//for (int i = 0; i < 6; i++)
		//{
		//	table.Rows.RemoveAt(0);
		//}
		//List<School> schools = new();
		//foreach (var row in table.AsEnumerable())
		//{
		//	School s = new ();
		//	s.Rspo = Convert.ToInt32(row.Field<string>(0));
		//	s.Regon = row.Field<string>(9) ?? string.Empty;
		//	s.SchoolType = row.Field<string>(10) ?? string.Empty;
		//	s.Name = row.Field<string>(11) ?? string.Empty;
		//	s.Address = row.Field<string>(20) ?? string.Empty;
		//	s.BuildingNumber = row.Field<string>(21) ?? string.Empty;
		//	s.FlatNumber = row.Field<string>(22) ?? string.Empty;
		//	s.Town = row.Field<string>(19) ?? string.Empty;
		//	s.PostCode = row.Field<string>(23) ?? string.Empty;
		//	s.Post = row.Field<string>(24) ?? string.Empty;
		//	schools.Add(s);
		//}
	}

	private List<School> _Parse(byte[] bytes)
	{
		string fileName = "C:\\Users\\ANI\\source\\repos\\HackYeah\\docs\\SIO 30.09.2022.xml";
		bytes = File.ReadAllBytes(fileName);
		// Create an XDocument from the byte array
		using MemoryStream stream = new(bytes);
		XNamespace ns = "http://example.org/namespace";

		XDocument xmlDoc = XDocument.Load(stream);

		// Specify the sheet name
		string sheetName = "Szkoły i placówki";

		//// Extract data from the XML
		//var schools = xmlDoc
		//	.Descendants("Worksheet")
		//	.FirstOrDefault(ws => ws.Attribute("Name")?.Value == sheetName)
		//	?.Descendants("Row")
		//	.Skip(5) // Skip header rows
		//	.Select(row => new School
		//	{
		//		Rspo = Convert.ToInt16(row.Field<string>(0)),
		//		Regon = row.Field<string>(1),
		//		SchoolType = Convert.ToInt32(row.Field<string>(2)),
		//		Name = row.Field<string>(3),
		//		Address = row.Field<string>(4),
		//		BuildingNumber = row.Field<string>(5),
		//		FlatNumber = row.Field<string>(6),
		//		Town = row.Field<string>(7),
		//		PostCode = row.Field<string>(8),
		//		Post = row.Field<string>(9)
		//	})
		//	.ToList();

		var schools = new List<School>();

		return schools;
	}
}
