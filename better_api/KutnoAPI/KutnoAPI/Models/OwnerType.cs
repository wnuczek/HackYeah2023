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
                case "Gmina": return OwnerType.COMMUNE;
                case "Fundacje": return OwnerType.FOUNDATIONS;
                case "Organizacje wynzaniowe": return OwnerType.RELIGION_ORGANISATION;
                case "Osoba fizyczna": return OwnerType.PHYSICAL_PERSON;
                case "Spółdzielnia": return OwnerType.COOPERATIVE;
                default: return OwnerType.None;
            }
        }
    }
}
