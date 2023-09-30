using KutnoAPI.Models;
using System.Data;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace KutnoAPI.Parsers;

public class XMLParser
{
	public List<School> ParseXmlToDataTable(string fileName, string sheetName)
	{
		// Load the XML data from the file
		XDocument xmlDoc = XDocument.Load(fileName);

		// Define the namespace mapping
		XNamespace ss = "urn:schemas-microsoft-com:office:spreadsheet";
		XNamespace excel = "urn:schemas-microsoft-com:office:excel";
		XNamespace html = "http://www.w3.org/TR/REC-html40";

		// Construct an XPath expression to select the rows from the specified worksheet
		string xpathExpression = $"//ss:Worksheet[@ss:Name='{sheetName}']//ss:Row";

		// Extract the data for the specified worksheet
		DataTable dataTable = new();
		for (int i = 0; i < 150; i++)
		{
			dataTable.Columns.Add();
		}
		bool columnsAdded = false;

		// Use LINQ to XML with namespaces
		var row = xmlDoc.Descendants(ss + "Worksheet")
			.Where(ws => (string)ws.Attribute(ss + "Name") == sheetName)
			.Descendants(ss + "Row")
			.Skip(5).Take(1);
		List<string> categories = new();
		var cells = row.Elements(ss + "Cell");
		foreach (var cell in cells)
		{
			var trimmed = cell.Value.Trim();
			if (trimmed.StartsWith('P') && Regex.IsMatch(trimmed, @"\d"))
			{
				categories.Add(trimmed);
			}
			
		}



		string breakpoint = "";

		var rows = xmlDoc.Descendants(ss + "Worksheet")
			.Where(ws => (string)ws.Attribute(ss + "Name") == sheetName)
			.Descendants(ss + "Row")
			.Skip(6)
			.Select(row =>
			{
				School s = new();
				var value = row.Elements(ss + "Cell");
				if (value.ElementAtOrDefault(9) is not null)
				{
					s.Rspo = Convert.ToInt32(value.ElementAtOrDefault(0).Value);
					s.Regon = value.ElementAtOrDefault(9).Value.ToString().Trim();
					s.SchoolType = value.ElementAtOrDefault(10).Value.ToString().Trim();
					s.Name = value.ElementAtOrDefault(11).Value.ToString().Trim();
					s.Address = value.ElementAtOrDefault(20).Value.ToString().Trim();
					s.BuildingNumber = value.ElementAtOrDefault(21).Value.ToString().Trim();
					s.FlatNumber = value.ElementAtOrDefault(22).Value.ToString().Trim();
					s.Town = value.ElementAtOrDefault(19).Value.ToString().Trim();
					s.PostCode = value.ElementAtOrDefault(23).Value.ToString().Trim();
					s.Post = value.ElementAtOrDefault(24).Value.ToString().Trim();
					s.Categories = new List<CategoryDefinition>();

					//foreach (var element in value.Elements())
					//{
					//	if (value.elem)
					//	CategoryDefinition cat = new();
					//	cat.
					//}
				}
				return s;
			})
			.Where(x => !string.IsNullOrEmpty(x.Regon))
			.ToList();

		//// Now you can work with the selected rows
		//foreach (var row in rows)
		//{
		//	DataRow dataRow = dataTable.NewRow();
		//	// Process each row as needed
		//	var cells = row.Elements(ss + ss+"Cell");
		//	var count = cells.Count();
		//	int columnIndex = 0;
		//	foreach (var cell in cells)
		//	{
		//		var data = cell.Element(ss + "Data");
		//		if (columnIndex < 150)
		//		{
		//			//string cellValue = (string)data;
		//			dataRow[columnIndex] = cell.Value.ToString().Trim();
		//			columnIndex++;
		//		} 
		//	}
		//	dataTable.Rows.Add(dataRow);
		//}

		return rows;
	}

	static XmlNamespaceManager XmlNamespaceManager(XDocument xmlDoc, XNamespace ss, XNamespace excel, XNamespace html)
	{
		XmlNamespaceManager nsManager = new XmlNamespaceManager(new NameTable());

		nsManager.AddNamespace("ss", ss.NamespaceName);
		nsManager.AddNamespace("excel", excel.NamespaceName);
		nsManager.AddNamespace("html", html.NamespaceName);

		return nsManager;
	}

}





