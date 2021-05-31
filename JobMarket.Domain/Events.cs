using System;
using System.Collections.Generic;
using System.Text;

namespace JobMarket.Domain
{
    public static class Events
    {
        public class JobAdCreated
        {
            public Guid Id { get; set; }
            public Guid OwnerId { get; set; }
        }

        public class JobAdTitleChanged
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
        }

        public class JobAdTextUpdated
        {
            public Guid Id { get; set; }
            public string AdText { get; set; }
        }

        public class JobAdSalaryUpdated
        {
            public Guid Id { get; set; }
            public decimal Salary { get; set; }
            public string CurrencyCode { get; set; }
        }

        public class JobAdSentForReview
        {
            public Guid Id { get; set; }
        }
    }
}
