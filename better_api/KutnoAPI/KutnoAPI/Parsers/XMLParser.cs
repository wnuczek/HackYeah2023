using KutnoAPI.Models;
using System.Data;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace KutnoAPI.Parsers;

public class XMLParser
{
	public List<School> ParseXmlToDataTable(string sheetName, byte[] schoolsWorksheet)
	{
		// Convert the byte array to a string
		string xmlString = Encoding.UTF8.GetString(schoolsWorksheet);

		// Parse the string into an XDocument
		XDocument xmlDoc = XDocument.Parse(xmlString);

		// Load the XML data from the file
		//XDocument xmlDoc = XDocument.Load(fileName);

		// Define the namespace mapping
		XNamespace ss = "urn:schemas-microsoft-com:office:spreadsheet";
		XNamespace excel = "urn:schemas-microsoft-com:office:excel";
		XNamespace html = "http://www.w3.org/TR/REC-html40";

		// Construct an XPath expression to select the rows from the specified worksheet
		string xpathExpression = $"//ss:Worksheet[@ss:Name='{sheetName}']//ss:Row";


		// Use LINQ to XML with namespaces
		var row = xmlDoc.Descendants(ss + "Worksheet")
			.Where(ws => (string)ws.Attribute(ss + "Name") == sheetName)
			.Descendants(ss + "Row")
			.Skip(5).Take(1);
		Dictionary< int, string> categories = new();
		var cells = row.Elements(ss + "Cell");
		for (int i = 0; i < cells.Count(); i++)
		{
			var cell = cells.ElementAt(i);
			var trimmed = cell.Value.Trim();
			if (trimmed.StartsWith('P') && Regex.IsMatch(trimmed, @"\d"))
			{
				categories.Add(i, trimmed);
			}
			
		}

		var schools = xmlDoc.Descendants(ss + "Worksheet")
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
					s.Summary = new();

					s.Summary.SchoolRSPO = s.Rspo;
					var test2 = value.ElementAt(33);
					CultureInfo culture = CultureInfo.InvariantCulture; 


					s.Summary.StudentsQuantity = decimal.Parse(value.ElementAtOrDefault(33).Value.ToString().Trim(), culture);

					s.Summary.StudentsFromCountryQuantity = decimal.Parse(value.ElementAtOrDefault(34).Value.ToString().Trim(), culture);
					s.Summary.StudentsFromSmallTownQuantity = decimal.Parse(value.ElementAtOrDefault(35).Value.ToString().Trim(), culture);
					s.Summary.StudentsOutsideSchool = decimal.Parse(value.ElementAtOrDefault(36).Value.ToString().Trim(), culture);		
					s.Categories = new ();

					foreach(var index in categories.Keys)
					{
						CategoryValues cat = new();
						var test = value.ElementAtOrDefault(index).Value.Trim();
						bool isParsed = decimal.TryParse(test, out decimal parsedValue);
						cat.Value =  isParsed ? parsedValue : 0;
						cat.SchoolRSPO = s.Rspo;
						cat.CategoryStr = categories[index];
						s.Categories.Add(cat);
					}

				}
				return s;
			})
			.Where(x => !string.IsNullOrEmpty(x.Regon))
			.ToList();

		var test = schools.Where(x => x.Summary.StudentsQuantity > 0).ToList();

		return schools;
	}

	//static XmlNamespaceManager XmlNamespaceManager(XDocument xmlDoc, XNamespace ss, XNamespace excel, XNamespace html)
	//{
	//	XmlNamespaceManager nsManager = new XmlNamespaceManager(new NameTable());

	//	nsManager.AddNamespace("ss", ss.NamespaceName);
	//	nsManager.AddNamespace("excel", excel.NamespaceName);
	//	nsManager.AddNamespace("html", html.NamespaceName);

	//	return nsManager;
	//}

}





