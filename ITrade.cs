using System;
using System.Collections.Generic;
using System.Text;

namespace CreditSuisseTrades
{
    public interface ITrade
    {
        double Value { get; set; } //indicates the transaction amount in dollars
        string ClientSector { get; set; } //indicates the client´s sector which can be "Public" or "Private"
        DateTime NextPaymentDate { get; set; } //indicates when the next payment from the client to the bank is expected
    }
}
