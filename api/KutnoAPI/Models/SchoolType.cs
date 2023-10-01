using System.Diagnostics.Eventing.Reader;

namespace KutnoAPI.Models
{
	public enum SchoolType
	{
		None = 0,
		KINDERGARDEN = 1,
		PRIMARY_SCHOOL,
		KINDERGARDEN_POINT,
	}
	
	public class SchoolTypeParser
	{
		public static SchoolType FromString(string value)
		{
			switch (value)
			{
				case "Przedszkole": return SchoolType.KINDERGARDEN;
                case "Szkoła podstawowa": return SchoolType.PRIMARY_SCHOOL;
				case "Punkt przedszkolny": return SchoolType.KINDERGARDEN_POINT;
				default: return SchoolType.None;	
            }
        }
	}
}
