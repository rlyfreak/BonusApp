namespace BonusApp.Models
{
    public class TransactionItem
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public string CafeName { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // "Начисление" или "Списание"
        public decimal BonusAmount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;

        public decimal BalanceBefore { get; set; }
        public decimal BalanceAfter { get; set; }
        public string VenueAddress { get; set; } = string.Empty;
        public string OperationCode { get; set; } = string.Empty;
        public string Comment { get; set;  } = string.Empty;

    }
}
