using System;
using System.Collections.Generic;
using System.Text;

namespace CreditSuisseTrades
{
    public class Trades : ITrade
    {
        public double Value { get; set; }
        public string ClientSector { get; set; }
        public DateTime NextPaymentDate { get; set; }
    }
}
