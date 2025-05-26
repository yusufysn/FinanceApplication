using DomainLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class Transaction : BaseEntity
    {
        public string FromAccountId { get; set; }
        public string ToAccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }

        public Account FromAccount { get; set; }
        public Account ToAccount { get; set; }
    }
}
