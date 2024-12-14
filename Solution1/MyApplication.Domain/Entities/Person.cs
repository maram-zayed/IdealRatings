namespace MyApplication.Domain.Entities;

public class Person : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string TelephoneCode { get; set; }
    public string TelephoneNumber { get; set; }
    public string Address { get; set; }
    public string Country { get; set; }
}
