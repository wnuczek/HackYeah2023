using KutnoAPI.Models;

namespace KutnoAPI.Parsers;

public class SchoolParser
{
	private readonly XMLParser xMLParser;

	public SchoolParser()
	{
		xMLParser = new XMLParser();
	}

	public List<School> Parse(SchoolUploadRequest schoolUploadRequest)
	{
		string sheetName = "Szkoły i placówki";

		var schools = xMLParser.ParseXmlToDataTableSchools(sheetName, schoolUploadRequest.SchoolsWorksheet);

		//foreach (var school in schools)
		//{
		//	foreach (var category in school.Categories)
		//	{
		//		//zaczytaj mnożnik
		//		//określ pomnożoną wartość
		//	}
		//	//Dodaj wszystkie wartości do suma P dla całej szkoły
		//}

		//Oblicz sumę dla wszystkich szkół

		//Określ procentowe udziały szkół

		return schools;
	}

}
