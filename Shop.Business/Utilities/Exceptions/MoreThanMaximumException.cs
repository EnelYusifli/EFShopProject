namespace Shop.Business.Utilities.Exceptions;

public class MoreThanMaximumException:Exception
{
    public MoreThanMaximumException(string message) : base(message)
    {

    }
}
