using System;
using System.Collections.Generic;
using System.Text;

namespace JobMarket.Domain
{
    public class Salary : Money
    {
        private Salary(decimal amount, string currencyCode, ICurrencyLookup currencyLookup)
            : base(amount, currencyCode, currencyLookup)
        {
            if (amount < 0)
                throw new ArgumentException(
                    "Salary cannot be negative",
                    nameof(amount));
        }

        internal Salary(decimal amount, string currencyCode)
            : base(amount, new Currency { CurrencyCode = currencyCode })
        {
        }

        public new static Salary FromDecimal(decimal amount, string currency,
            ICurrencyLookup currencyLookup) =>
            new Salary(amount, currency, currencyLookup);
    }
}
