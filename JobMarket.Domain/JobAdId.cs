using System;
using System.Collections.Generic;
using System.Text;

namespace JobMarket.Domain
{
    public class JobAdId : IEquatable<JobAdId>
    {
        private Guid Value { get; }

        public JobAdId(Guid value)
        {
            if (value == default)
                throw new ArgumentNullException(nameof(value), "Job Ad id cannot be empty");

            Value = value;
        }

        public static implicit operator Guid(JobAdId self) => self.Value;

        public static implicit operator JobAdId(string value)
            => new JobAdId(Guid.Parse(value));

        public override string ToString() => Value.ToString();

        public bool Equals(JobAdId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((JobAdId)obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
