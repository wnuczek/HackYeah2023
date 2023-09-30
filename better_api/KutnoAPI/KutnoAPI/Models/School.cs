namespace KutnoAPI.Models;

public class School
{
	public int Rspo { get; set; }
	public string Regon { get; set; }
	public int SchoolType { get; set; }
	public string Name { get; set; }
	public string Address { get; set; }
	public string BuildingNumber { get; set; }
	public string FlatNumber { get; set; }
	public string Town { get; set; }
	public string PostCode { get; set; }
	public string Post { get; set; }
	public OwnerType Owner { get; set; }

}

