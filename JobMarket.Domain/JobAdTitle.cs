using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using JobMarket.Library;

namespace JobMarket.Domain
{
    public class JobAdTitle : Value<JobAdTitle>
    {
        public static JobAdTitle FromString(string title)
        {
            CheckValidity(title);
            return new JobAdTitle(title);
        }

        public static JobAdTitle FromHtml(string htmlTitle)
        {
            var supportedTagsReplaced = htmlTitle
                .Replace("<i>", "*")
                .Replace("</i>", "*")
                .Replace("<b>", "**")
                .Replace("</b>", "**");

            var value = Regex.Replace(supportedTagsReplaced, "<.*?>", string.Empty);
            CheckValidity(value);

            return new JobAdTitle(value);
        }

        public string Value { get; }

        internal JobAdTitle(string value) => Value = value;

        public static implicit operator string(JobAdTitle title) =>
            title.Value;

        private static void CheckValidity(string value)
        {
            if (value.Length > 100)
                throw new ArgumentOutOfRangeException(
                    "Title cannot be longer that 100 characters",
                    nameof(value));
        }
    }
}
