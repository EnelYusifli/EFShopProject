namespace Shop.Business.Utilities.Exceptions;

public class ShouldBeUniqueException:Exception
{
    public ShouldBeUniqueException(string message) : base(message)
    {

    }
}
