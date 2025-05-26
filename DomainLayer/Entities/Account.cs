using DomainLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class Account : BaseEntity
    {
        public AppUser User { get; set; }
        public string AccountName { get; set; }
        public string IBAN { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; } = "TRY";
        public bool IsActive { get; set; } = true;
    }
}
