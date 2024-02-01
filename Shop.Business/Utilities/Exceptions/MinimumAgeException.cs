namespace Shop.Business.Utilities.Exceptions;

public class LessThanMinimumException:Exception
{
    public LessThanMinimumException(string message) : base(message)
    {

    }
}
