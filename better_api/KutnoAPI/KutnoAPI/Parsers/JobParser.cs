using KutnoAPI.Models;

namespace KutnoAPI.Parsers;

public class JobParser
{
	private readonly XMLParser xMLParser;

	public JobParser()
	{
		xMLParser = new XMLParser();
	}

	public List<JobSummary> Parse(SchoolUploadRequest schoolUploadRequest)
	{
		string sheetName = "Arkusz1";

		var jobs = xMLParser.ParseXmlToDataTableJobs(sheetName, schoolUploadRequest.JobsWorksheet);

		//Oblicz sumę dla wszystkich szkół

		//Określ procentowe udziały szkół

		return jobs;
	}

}
