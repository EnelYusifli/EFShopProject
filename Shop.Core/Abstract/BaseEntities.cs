namespace Shop.Core.Abstract;

public abstract class BaseEntities
{
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime ModifiedTime { get; set; } = DateTime.Now;
    public bool IsDeactive { get; set; } = false;
}
