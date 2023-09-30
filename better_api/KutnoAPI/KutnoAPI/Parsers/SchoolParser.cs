using KutnoAPI.Models;
using System.Xml.Linq;

namespace KutnoAPI.Parsers;

public class SchoolParser
{
	private readonly XMLParser xMLParser;

	public SchoolParser()
	{
		xMLParser = new XMLParser();
	}

	//public void Parse(byte[] bytes)
	//{
	//	string fileName = "C:\\Users\\ANI\\source\\repos\\HackYeah\\docs\\SIO 30.09.2022.xml";
	//	string sheetName = "Szkoły i placówki";

	//	var result = xMLParser.ParseXmlToDataTable(fileName, sheetName);
	//}

	public List<School> Parse(byte[] bytes)
	{
		string fileName = "C:\\Users\\ANI\\source\\repos\\HackYeah\\docs\\SIO 30.09.2022.xml";
		bytes = File.ReadAllBytes(fileName);
		// Create an XDocument from the byte array
		using MemoryStream stream = new(bytes);

		XDocument xmlDoc = XDocument.Load(stream);

		// Specify the sheet name
		string sheetName = "Szkoły i placówki";

		// Extract data from the XML
		var schools = xmlDoc
			.Descendants("Worksheet")
			.FirstOrDefault(ws => ws.Attribute("Name")?.Value == sheetName)
			?.Descendants("Row")
			.Skip(5) // Skip header rows
			.Select(row => new School
			{
				Rspo = Convert.ToInt16(row.Elements("Cell").ElementAt(0).Value),
				Regon = row.Elements("Cell").ElementAt(1).Value,
				SchoolType = Convert.ToInt32(row.Elements("Cell").ElementAt(2).Value),
				Name = row.Elements("Cell").ElementAt(3).Value,
				Address = row.Elements("Cell").ElementAt(4).Value,
				BuildingNumber = row.Elements("Cell").ElementAt(5).Value,
				FlatNumber = row.Elements("Cell").ElementAt(6).Value,
				Town = row.Elements("Cell").ElementAt(7).Value,
				PostCode = row.Elements("Cell").ElementAt(8).Value,
				Post = row.Elements("Cell").ElementAt(9).Value
			})
			.ToList();

		return schools;
	}
}
