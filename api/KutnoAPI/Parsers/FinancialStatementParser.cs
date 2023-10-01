using KutnoAPI.Models;
using System.Globalization;
using System.Text;
using System.Xml.Linq;

namespace KutnoAPI.Parsers;

public class FinancialStatementParser
{
	internal List<FinancialSummary> Parse(byte[] summary, bool costs)
	{
		string xmlString = Encoding.UTF8.GetString(summary);
		string _byteOrderMarkUtf8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
		if (xmlString.StartsWith(_byteOrderMarkUtf8))
		{
			xmlString = xmlString.Remove(0, _byteOrderMarkUtf8.Length);
		}

		XDocument xmlDoc = XDocument.Parse(xmlString);

		List<FinancialSummary> financials = new();

		var jednostki = xmlDoc.Descendants("Jednostka");
		foreach (var jednostka in jednostki)
		{
			string regon = GetString(jednostka.Element("Regon"));

			var pozycje = jednostka.Descendants("Pozycja").Select(pozycjaElement =>
			{
				FinancialSummary output ;
				if (costs)
				{
					output = ParseCosts(pozycjaElement);
				}
				else
				{
					output = ParseIncome(pozycjaElement);
				}

				output.Regon = regon;
				return output;
			}).ToList();
			financials.AddRange(pozycje);

		}
		return financials;
	}

	private FinancialSummary ParseIncome(XElement? pozycjaElement)
	{
		var output = new FinancialSummary();
		if (pozycjaElement is not null)
		{
			output.Department = GetString(pozycjaElement.Element("Dzial"));
			output.Chapter = GetString(pozycjaElement.Element("Rozdzial"));
			output.Paragraph = GetInt(pozycjaElement.Element("Paragraf"));
			output.PlannedIncome = GetDecimal(pozycjaElement.Element("PL"));
			output.Receivables = GetDecimal(pozycjaElement.Element("NA"));
			output.MadeIncome = GetDecimal(pozycjaElement.Element("DW"));

		}
		return output;
	}
	private FinancialSummary ParseCosts(XElement? pozycjaElement)
	{
		var output = new FinancialSummary();
		if (pozycjaElement is not null)
		{
			output.Department = GetString(pozycjaElement.Element("Dzial"));
			output.Chapter = GetString(pozycjaElement.Element("Rozdzial"));
			output.Paragraph = GetInt(pozycjaElement.Element("Paragraf"));
			output.PlannedCost = GetDecimal(pozycjaElement.Element("PL"));
			output.Engagement = GetDecimal(pozycjaElement.Element("ZA"));
			output.MadeCost = GetDecimal(pozycjaElement.Element("WW"));

		}
		return output;
	}

	private string GetString(XElement? element)
	{
		if (element is null)
			return string.Empty;

		return element.Value.Trim();

	}
	private int GetInt(XElement? str)
	{
		if (str is null)
			return 0;

		var isParsed = int.TryParse(str.Value, out int output);

		return isParsed ? output : 0;

	}
	private decimal GetDecimal(XElement? str)
	{
		if (str is null)
			return 0;

		CultureInfo culture = CultureInfo.InvariantCulture;
		var isParsed = decimal.TryParse(str.Value, NumberStyles.Number, culture, out decimal output);

		return isParsed ? output : 0;
	}
}
