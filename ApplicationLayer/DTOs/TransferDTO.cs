using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTOs
{
    public class TransferDTO
    {
        public string FromAccountName { get; set; }
        public string IBAN { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}
