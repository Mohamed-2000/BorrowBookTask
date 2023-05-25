namespace MyBase.Domain.Entities;

public class Employee : BaseEntity
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string NameEn { get; set; }

    public string Phone { get; set; }
    public string Image { get; set; }


    //public virtual AppUser AppUser { get; set; }

}

