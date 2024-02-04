using Shop.Business.Interfaces;
using Shop.Business.Utilities.Exceptions;
using Shop.Core.Entities;
using Shop.DataAccess;

namespace Shop.Business.Services;

public class WalletService : IWalletService
{
    ShopDbContext context = new ShopDbContext();
    public void CreateCard(User user, string cardNumber, string cvc, decimal balance)
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
                    context.Wallets.Add(wallet);
                    context.SaveChanges();
                    Console.Out.WriteLine("Successfully added");
                }
                else throw new AlreadyExistException("This card is already added");
            }
            else throw new LessThanMinimumException("Balance cannot be negative");
        }
        else throw new CannotBeNullException("Value cannot be null");

    }
    public void DeactivateWallet(int walletId,User user)
    {
        Wallet wallet=context.Wallets.Find(walletId);
        if (wallet is not null)
        {
            if (wallet.IsDeactive == false)
            {
                wallet.IsDeactive = true;
                wallet.ModifiedTime = DateTime.Now;
                context.SaveChanges();
                Console.WriteLine("Successfully Deactivated");
            }
            else throw new AlreadyExistException("Wallet is already deactive");
        }
        else throw new CannotBeFoundException("Wallet cannot be found");
    }
    public void ActivateWallet(int walletId, User user)
    {
        Wallet wallet = context.Wallets.Find(walletId);
        if (wallet is not null)
        {

            if (wallet.IsDeactive == true)
            {
                wallet.IsDeactive = false;
                wallet.ModifiedTime = DateTime.Now;
                context.SaveChanges();
                Console.WriteLine("Successfully Activated");
            }
            else throw new AlreadyExistException("Wallet is already Active");
        }
        else throw new CannotBeFoundException("Wallet cannot be found");
    }
}
