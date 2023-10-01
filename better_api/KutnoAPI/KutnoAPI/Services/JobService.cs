using KutnoAPI.Models;
using Npgsql;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System;
using Dapper;

namespace KutnoAPI.Services
{
    public class JobService
    {
        private const string INSERT_JOBS_HEADER = "INSERT INTO pgj.job_summary(schoolrspo, year, nopromotiongradedquantity, nominatedquantity, promotiongradedquantity)" +
            " VALUES (@SchoolRSPO, @Year, @NoPromotionGradedQuantity, @NominatedQuantity, @PromotionGradedQuantity)" +
            " ON CONFLICT (schoolrspo, year) DO UPDATE SET nopromotiongradedquantity=EXCLUDED.nopromotiongradedquantity, promotiongradedquantity=EXCLUDED.promotiongradedquantity, nominatedquantity=EXCLUDED.nominatedquantity;";



        public bool InsertJobsData(List<JobSummary> jobs, NpgsqlConnection connection)
        {
            //using (var tx = connection.BeginTransaction())
            //{
            foreach(var j in jobs)
            {
                connection.Execute(INSERT_JOBS_HEADER, j);

            }
            return true;
            //}
        }

    }
}
