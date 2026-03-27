using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BonusApp.Models
{
    public class TransactionItem
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public string Type { get; set; } = string.Empty; // "Начисление" или "Списание"
        public decimal BonusAmount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
