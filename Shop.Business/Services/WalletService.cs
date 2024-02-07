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
    public void DeactivateWallet(int walletId, User user)
    {
        if (user is not null && user.IsDeactive == false)
        {
            bool hasWallet = context.Wallets.Where(w => w.Id == walletId && w.UserId == user.Id).Any();
            if (hasWallet)
            {
                Wallet wallet = context.Wallets.Find(walletId);
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
            else throw new CannotBeFoundException("Wallet cannot be found");

        }
        else throw new CannotBeFoundException("User cannot be found");
    }
    
    public void ActivateWallet(int walletId, User user)
{
        if (user is not null && user.IsDeactive == false)
        {
            bool hasWallet = context.Wallets.Where(w => w.Id == walletId && w.UserId == user.Id).Any();
            if (hasWallet)
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
                    else throw new AlreadyExistException("Wallet is already active");
                }
                else throw new CannotBeFoundException("Wallet cannot be found");

            }
            else throw new CannotBeFoundException("Wallet cannot be found");

        }
        else throw new CannotBeFoundException("User cannot be found");
    }

public void IncreaseBalance(int walletId, User user,decimal amount)
{
        if (user is not null && user.IsDeactive == false)
        {
            if (amount > 0)
            {
                bool hasWallet = context.Wallets.Where(w => w.Id == walletId && w.UserId == user.Id).Any();
                if (hasWallet)
                {
                    Wallet wallet = context.Wallets.Find(walletId);
                    if (wallet is not null && wallet.IsDeactive == false)
                    {
                        wallet.Balance += amount;
                        wallet.ModifiedTime=DateTime.Now;
                        context.SaveChanges();
                        Console.WriteLine("Successfully increased");
                    }
                    else throw new CannotBeFoundException("Wallet cannot be found");

                }
                else throw new CannotBeFoundException("Wallet cannot be found");

            }
            else throw new LessThanMinimumException("Value cannot be negative or 0");

        }
        else throw new CannotBeFoundException("User cannot be found");
    }
    public void TransferMoney(int walletIdToIncrease,int walletIdToTransfer,User user,decimal transferAmount)
    {
        if (user is not null && user.IsDeactive == false)
        {
            if (transferAmount > 0)
            {
                bool hasWallet = context.Wallets.Where(w => w.Id == walletIdToIncrease && w.UserId == user.Id).Any();
                bool hasWalletToTranser = context.Wallets.Where(w => w.Id == walletIdToTransfer && w.UserId == user.Id).Any();
                if (hasWallet && hasWalletToTranser)
                {
                    Wallet walletToIncrease = context.Wallets.Find(walletIdToIncrease);
                    Wallet walletToTransfer = context.Wallets.Find(walletIdToTransfer);
                    if (walletToIncrease != walletToTransfer)
                    {
                        if (walletToIncrease is not null && walletToIncrease.IsDeactive == false && walletToTransfer is not null && walletToTransfer.IsDeactive == false)
                        {
                            if (transferAmount > walletToTransfer.Balance)
                                throw new MoreThanMaximumException("You do not have that much money in the card");
                            walletToIncrease.Balance += transferAmount;
                            walletToTransfer.Balance -= transferAmount;
                            walletToIncrease.ModifiedTime = DateTime.Now;
                            walletToTransfer.ModifiedTime = DateTime.Now;
                            context.SaveChanges();
                            Console.WriteLine("Successfully transferred");
                        }
                        else throw new CannotBeFoundException("Wallet cannot be found");

                    }
                    else throw new ShouldBeUniqueException("Cards cannot be the same");

                }
                else throw new CannotBeFoundException("Wallet cannot be found");

            }
            else throw new LessThanMinimumException("Value cannot be negative or 0");

        }
        else throw new CannotBeFoundException("User cannot be found");
    }

    public void PayForInvoice(int walletId, User user, decimal amount)
    {
        Wallet wallet = context.Wallets.FirstOrDefault(w => w.Id == walletId && w.UserId == user.Id);
        if (amount <= 0) throw new LessThanMinimumException("Your cart is empty");
        if (wallet is not null && wallet.IsDeactive==false)
        {
            if (wallet.Balance < amount)
                throw new LessThanMinimumException("Balance of this wallet is not enough");
            wallet.Balance -= amount;
            wallet.ModifiedTime = DateTime.Now;
            context.Entry(wallet).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        else throw new CannotBeFoundException("You do not have any saved card");
    }
    public void ShowUserWallets(User user, ShopDbContext context)
    {
        var userWallets = context.Wallets.Where(w => w.UserId == user.Id && !w.IsDeactive).ToList();

        if (userWallets.Any())
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            foreach (var wallet in userWallets)
            {
                Console.WriteLine($"Id:{wallet.Id}/Card: {wallet.Number}\n" +
                                  $"Balance: {wallet.Balance}\n");
            }
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("You don't have any cards yet.");
            Console.ResetColor();
        }
    }
}
