using Shop.Core.Entities;

namespace Shop.Business.Interfaces;

public interface IWalletService
{
    void CreateCard(User user, string cardNumber, string cvc, decimal balance);
    void DeactivateWallet(int walletId, User user);
    void ActivateWallet(int walletId, User user);
    void IncreaseBalance(int walletId, User user, decimal amount);
    void TransferMoney(int walletIdToIncrease, int walletIdToTransfer, User user, decimal transferAmount);
    void PayForInvoice(int walletId, User user, decimal amount);
}
