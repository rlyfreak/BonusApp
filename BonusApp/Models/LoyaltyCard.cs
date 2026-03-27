using System;
using System.Collections.Generic;
using System.Text;

namespace BonusApp.Models
{
    public class LoyaltyCard
    {
        public int Id { get; set; }
        public string CafeName { get; set; } = string.Empty;
        public string CardNumber { get; set; } = string.Empty;
        public decimal BonusBalance { get; set; }
        public string QrCodeValue { get; set; } = string.Empty;
    }
}
