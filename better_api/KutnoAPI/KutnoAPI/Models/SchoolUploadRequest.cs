namespace KutnoAPI.Models
{
    public class SchoolUploadRequest
    {
        public int Year { get; set; }

        public byte[] SchoolsWorksheet { get; set; }
        public byte[] JobsWorksheet { get; set; }

    }
}
