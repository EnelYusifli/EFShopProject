namespace Shop.Business.Utilities.Exceptions;

public class CannotBeFoundException:Exception
{
    public CannotBeFoundException(string message) : base(message)
    {
    }
}
