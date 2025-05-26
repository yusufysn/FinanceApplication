using DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTOs
{
    public class CreateUserAccountDTO
    {
        public decimal Balance { get; set; }
        public string AccountName { get; set; }

        //public string UserId { get; set; }
    }
}
