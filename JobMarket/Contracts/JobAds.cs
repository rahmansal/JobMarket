using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobMarket.Contracts
{
    public static class JobAds
    {
        public static class V1
        {
            public class Create
            {
                public Guid Id { get; set; }
                public Guid OwnerId { get; set; }
            }

            public class SetTitle
            {
                public Guid Id { get; set; }
                public string Title { get; set; }
            }

            public class UpdateText
            {
                public Guid Id { get; set; }
                public string Text { get; set; }
            }

            public class UpdateSalary
            {
                public Guid Id { get; set; }
                public decimal Salary { get; set; }
                public string Currency { get; set; }
            }

            public class RequestToPublish
            {
                public Guid Id { get; set; }
            }
        }
    }
}
