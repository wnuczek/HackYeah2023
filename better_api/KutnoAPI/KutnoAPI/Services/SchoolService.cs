using KutnoAPI.Models;
using Npgsql;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System;
using Dapper;

namespace KutnoAPI.Services
{
    public class SchoolService
    {
        private const string INSERT_SCHOOL_HEADER = "INSERT INTO pgj.schools(rspo, regon, schooltype, name, address, buildingnumber, flatnumber, town, postcode, post, ownertype)" +
            " VALUES (@RSPO, @Regon, @SchoolType, @Name, @Address, @BuildingNumber, @FlatNumber, @Town, @Postcode, @Post, @OwnerType)" +
            " ON CONFLICT (rspo) DO UPDATE SET regon=EXCLUDED.regon, schooltype=EXCLUDED.schooltype, name=EXCLUDED.name, address=EXCLUDED.address, buildingnumber=EXCLUDED.buildingnumber, flatnumber=EXCLUDED.flatnumber, town=EXCLUDED.town, postcode=EXCLUDED.postcode, post=EXCLUDED.post, ownertype=EXCLUDED.ownertype;";

        private const string INSERT_SCHOOL_SUMMARY = "INSERT INTO pgj.school_summary(schoolrspo, year, studentsquantity, studentsfromcountryquantity, studentsfromsmalltownquantity, studentsoutsideschool)" +
            " VALUES (@SchoolRSPO, @Year, @StudentsQuantity, @StudentsFromCountryQuantity, @StudentsFromSmallTownQuantity, @StudentsOutsideSchool)" +
            " ON CONFLICT (schoolrspo, year) DO UPDATE SET studentsquantity=EXCLUDED.studentsquantity, studentsfromcountryquantity=EXCLUDED.studentsfromcountryquantity, studentsfromsmalltownquantity=EXCLUDED.studentsfromsmalltownquantity, studentsoutsideschool=EXCLUDED.studentsoutsideschool";

        public bool InsertSchoolData(List<School> schools, NpgsqlConnection connection)
        {
            //using (var tx = connection.BeginTransaction())
            //{
            foreach(School school in schools)
            {
                connection.Execute(INSERT_SCHOOL_HEADER, school);
                connection.Execute(INSERT_SCHOOL_SUMMARY, school.Summary);

            }
            return true;
            //}
        }

    }
}
