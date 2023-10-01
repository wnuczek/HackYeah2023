using KutnoAPI.Models;
using System.Data;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml.XPath;

namespace KutnoAPI.Parsers;

public class XMLParser
{
	public List<School> ParseXmlToDataTableSchools(string sheetName, byte[] schoolsWorksheet)
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
				var rowDetails = row.Elements(ss + "Cell");
				if (rowDetails.ElementAtOrDefault(9) is not null)
				{
					s.Rspo = Convert.ToInt32(GetString(rowDetails,0));
					s.Regon = GetString(rowDetails, 9);
					s.SchoolType = SchoolTypeParser.FromString(GetString(rowDetails, 10));
					s.Name = GetString(rowDetails, 11);
					s.Address = GetString(rowDetails, 20);
					s.BuildingNumber = GetString(rowDetails, 21);
					s.FlatNumber = GetString(rowDetails, 22);
					s.Town = GetString(rowDetails, 19);
					s.PostCode = GetString(rowDetails, 23);
					s.Post = GetString(rowDetails, 24);
					s.OwnerType = OwnerTypeParser.FromString(GetString(rowDetails, 26));
                    s.Summary = new();

					s.Summary.SchoolRSPO = s.Rspo;
					var test2 = rowDetails.ElementAt(33);
					


					s.Summary.StudentsQuantity = GetDecimal(rowDetails, 33);

					s.Summary.StudentsFromCountryQuantity = GetDecimal(rowDetails, 34);
					s.Summary.StudentsFromSmallTownQuantity = GetDecimal(rowDetails, 35);
					s.Summary.StudentsOutsideSchool = GetDecimal(rowDetails, 36);		
					s.Categories = new ();

					foreach(var index in categories.Keys)
					{
						CategoryValues cat = new();
						cat.Value =  GetDecimal(rowDetails, index);
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

    public List<JobSummary> ParseXmlToDataTableJobs(string sheetName, byte[] jobsWorksheet)
    {
        // Convert the byte array to a string
        string xmlString = Encoding.UTF8.GetString(jobsWorksheet);

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
        Dictionary<int, string> categories = new();
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
                JobSummary j = new();
                var rowDetails = row.Elements(ss + "Cell");
                if (rowDetails.ElementAtOrDefault(9) is not null)
                {
                    j.SchoolRspo = Convert.ToInt32(GetString(rowDetails, 0));
					j.NoPromotionGradedQuantity = Convert.ToDecimal(GetString(rowDetails, 10));
					j.NominatedQuantity = Convert.ToDecimal(GetString(rowDetails,11));
					j.PromotionGradedQuantity = Convert.ToDecimal(GetString(rowDetails, 12));
                }
                return j;
            })
            .Where(x => x.SchoolRspo > 0)
            .ToList();
        return schools;
    }

    private string GetString(IEnumerable<XElement> elements, int index)
	{
		if (elements.ElementAtOrDefault(index) is null)
			return string.Empty;

		return elements.ElementAt(index).Value.Trim();
	}
	private decimal GetDecimal(IEnumerable<XElement> elements, int index)
	{
		if (elements.ElementAtOrDefault(index) is null)
			return 0;

		CultureInfo culture = CultureInfo.InvariantCulture;
		var isParsed = decimal.TryParse(GetString(elements, index), NumberStyles.Number, culture, out decimal output);

		return isParsed ? output : 0;
	}

}





