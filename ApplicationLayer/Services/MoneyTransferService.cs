using ApplicationLayer.DTOs;
using DomainLayer.Entities;
using DomainLayer.Repositories;
using InfrastructureLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Services
{
    public class MoneyTransferService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MoneyTransferService(IAccountRepository accountRepository, ITransactionRepository transactionRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task TransferAsync(TransferDTO request, string FromAccountId)
        {
            // Transaction başlatılır (unit of work kullanarak)
            using var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                var senderAccount = await _unitOfWork.AccountRepository.GetSingleAsync(x=>x.AccountName == request.FromAccountName);
                if (senderAccount == null)
                    throw new Exception("Sender account not found");

                if (senderAccount.Balance < request.Amount)
                    throw new Exception("Insufficient balance");

                var receiverAccount = await _unitOfWork.AccountRepository.GetSingleAsync(x=>x.IBAN == request.IBAN);
                if (receiverAccount == null)
                    throw new Exception("Receiver not found or name mismatch");

                senderAccount.Balance -= request.Amount;
                receiverAccount.Balance += request.Amount;

                _unitOfWork.AccountRepository.Update(senderAccount);
                _unitOfWork.AccountRepository.Update(receiverAccount);
                await _unitOfWork.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
