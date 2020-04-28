using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierMvc.Model.Application
{
    public class PaymentFailedPendingTransactionsList
    {
        public Int64 PaymentDetailId { get; set; }
        public string RegistrationNumber { get; set; }
        public string BRN { get; set; }
        public string STC { get; set; }
        public string RMK { get; set; }
    }
}
