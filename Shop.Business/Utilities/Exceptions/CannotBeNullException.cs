namespace Shop.Business.Utilities.Exceptions;

public class CannotBeNullException:Exception
{
    public CannotBeNullException(string message):base(message)
    {
    }
}
