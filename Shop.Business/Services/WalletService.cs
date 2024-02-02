using Shop.Business.Interfaces;
using Shop.Business.Utilities.Exceptions;
using Shop.Core.Entities;
using Shop.DataAccess;

namespace Shop.Business.Services;

public class WalletService : IWalletService
{
    ShopDbContext context = new ShopDbContext();
    public async void CreateCard(User user, string cardNumber, string cvc, decimal balance)
    {
        if (user is not null || cardNumber is not null || cvc is not null)
        {
            if (balance >= 0)
            {
                bool isDublicate = context.Wallets.Where(w => w.Number.ToLower() == cardNumber.ToLower()).Any();
                if (!isDublicate)
                {
                    Wallet wallet = new()
                    {
                        UserId = user.Id,
                        Number = cardNumber,
                        CVC = cvc,
                        Balance = balance
                    };
                    await context.Wallets.AddAsync(wallet);
                    await context.SaveChangesAsync();
                } else throw new AlreadyExistException("This card is already added");
            }
            else throw new LessThanMinimumException("Balance cannot be negative");
        }
        else throw new CannotBeNullException("Value cannot be null");

    }
}
