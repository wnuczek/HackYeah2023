namespace KutnoAPI.Models
{
    public class ExamsSummary
    {
        public long SchoolRSPO { get; set; }
        public string Name { get; set; }
        public decimal StudentsQuantity { get; set; }
        public decimal Average { get; set; }
        public decimal StandardDeviation { get; set; }
        public decimal Median { get; set; }
        public decimal Modal { get; set; }
    }
}
