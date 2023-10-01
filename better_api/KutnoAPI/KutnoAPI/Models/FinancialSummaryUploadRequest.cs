namespace KutnoAPI.Models
{
    public class FinancialSummaryUploadRequest
	{
        public int Year { get; set; }
        public byte[] IncomeSummary { get; set; }
        public byte[] CostsSummary { get; set; }

    }
}
