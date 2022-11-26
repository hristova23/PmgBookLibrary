using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Models.Transaction
{
    public class TransactionViewModel
    {
        public int Id { get; set; }

        public string SenderEmail { get; set; }

        public string RecieverEmail { get; set; }

        public int Quantity { get; set; }

        public string? Message { get; set; }

        public DateTime Date { get; set; }
    }
}
