using ProductService.Domain.Common;

namespace ProductService.Domain.Entities;

public class Category //: AuditableEntity
{
    public Guid ID { get; private set; }
    //public Guid? ParentCategoryID { get; private set; }

    public string Name { get; private set; } = null!;
    //public string Slug { get; private set; } = null!;
    //public bool IsActive { get; private set; }

   // public Category? Parent { get; private set; }
    //public ICollection<Category> Children { get; private set; }
    //    = new List<Category>();

    private Category() { }

    public Category(string name)
    {
        ID = Guid.NewGuid();
        Name = name;
        //IsActive = true;
    }
    public void Update(string name)
    {
        Name = name;
        //MarkUpdated();
    }
}

