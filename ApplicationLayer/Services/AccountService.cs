using ApplicationLayer.DTOs;
using DomainLayer.Entities;
using DomainLayer.Repositories;
using InfrastructureLayer.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Services
{
    public class AccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;

        public AccountService(IAccountRepository accountRepository, UserManager<AppUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public IQueryable<Account> GetAllAccounts(string UserId) => _unitOfWork.AccountRepository.GetWhere(x=>x.User.Id == UserId);

        public async Task CreateAccount(string UserId, CreateUserAccountDTO request)
        {
            var iban = GenerateIban();
            var account = new Account {
                User = (await _userManager.FindByIdAsync(UserId)),
                AccountName = request.AccountName,
                Balance = request.Balance,
                IBAN = iban,
                IsActive = true
            };
            await _unitOfWork.AccountRepository.AddAsync(account);
            await _unitOfWork.CommitAsync();
        }

        private string GenerateIban()
        {
            var random = new Random();

            string countryCode = "TR"; 
            string controlNumber = random.Next(10, 99).ToString(); 
            string bankCode = "00061"; 

            string accountNumber = random.NextInt64(1_000_000_000_000_0000, 9_999_999_999_999_9999).ToString(); 

            return $"{countryCode}{controlNumber}{bankCode}{accountNumber}";
        }
    }
}
