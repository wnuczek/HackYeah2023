using System.Data;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace KutnoAPI.Parsers;

public class XMLParser
{
	public DataTable ParseXmlToDataTable(string fileName, string sheetName)
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
		DataTable dataTable = new DataTable();
		bool columnsAdded = false;

		foreach (XElement row in xmlDoc.XPathSelectElements(xpathExpression, XmlNamespaceManager(xmlDoc, ss, excel, html)))
		{
			DataRow dataRow = dataTable.NewRow();

			int columnIndex = 0;
			foreach (XElement cell in row.XPathSelectElements(".//ss:Data", XmlNamespaceManager(xmlDoc, ss, excel, html)))
			{
				if (!columnsAdded)
				{
					dataTable.Columns.Add($"Column{columnIndex}");
				}

				dataRow[columnIndex] = cell.Value;
				columnIndex++;
			}

			if (!columnsAdded)
			{
				columnsAdded = true;
			}

			dataTable.Rows.Add(dataRow);
		}

		return dataTable;
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





