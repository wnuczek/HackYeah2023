namespace KutnoAPI.Models
{
	public enum OwnerType
	{
        None = 0,
		COMMUNE = 1,
		RELIGION_ORGANISATION = 2,
		PHYSICAL_PERSON = 3,
		COOPERATIVE = 4,
		FOUNDATIONS = 5
	}

    public class OwnerTypeParser
    {
        public static OwnerType FromString(string value)
        {
            switch (value)
            {
                default: return OwnerType.None;
            }
        }
    }
}
