using System;
using System.Collections.Generic;
using System.Text;
using JobMarket.Library;

namespace JobMarket.Domain
{
    public class JobAdText : Value<JobAdText>
    {
        public string Value { get; }

        internal JobAdText(string text) => Value = text;

        public static JobAdText FromString(string text) =>
            new JobAdText(text);

        public static implicit operator string(JobAdText text) =>
            text.Value;
    }
}
