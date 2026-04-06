using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace BonusApp.Models
{
    public class HistoryRecord
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public string CafeName { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public decimal BonusAmount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;

        public bool IsAccrual => Type == "Начисление";

        public string SignedAmount => IsAccrual
            ? $"+{BonusAmount:0}"
            : $"-{BonusAmount:0}";

        public string TimeText => Date.ToString("HH:mm");

        public string DateGroupTitle
        {
            get
            {
                var today = DateTime.Today;
                var date = Date.Date;

                if (date == today)
                    return "Сегодня";

                if (date == today.AddDays(-1))
                    return "Вчера";

                return Date.ToString("d MMMM", new CultureInfo("ru-RU"));
            }
        }

        public string AmountColorHex => IsAccrual ? "#8FE39C" : "#FF6B6B";
    }
}

