using System;
using System.Collections.Generic;
using System.Text;
using JobMarket.Domain;
using Xunit;

namespace JobMarket.Tests
{
    public class JobAd_Publish_Spec
    {
        private readonly JobAd _jobAd;

        public JobAd_Publish_Spec()
        {
            _jobAd = new JobAd(
                new JobAdId(Guid.NewGuid()),
                new UserId(Guid.NewGuid()));
        }

        [Fact]
        public void Can_publish_a_valid_ad()
        {
            _jobAd.SetTitle(JobAdTitle.FromString("Test ad"));
            _jobAd.UpdateText(JobAdText.FromString("Job Description"));
            _jobAd.UpdateSalary(
                Salary.FromDecimal(100.10m, "EUR", new FakeCurrencyLookup()));

            _jobAd.RequestToPublish();

            Assert.Equal(JobAd.JobAdState.PendingReview,
                _jobAd.State);
        }

        [Fact]
        public void Cannot_publish_without_title()
        {
            _jobAd.UpdateText(JobAdText.FromString("Job Description"));
            _jobAd.UpdateSalary(
                Salary.FromDecimal(100.10m, "EUR", new FakeCurrencyLookup()));

            Assert.Throws<InvalidEntityStateException>(() => _jobAd.RequestToPublish());
        }

        [Fact]
        public void Cannot_publish_without_text()
        {
            _jobAd.SetTitle(JobAdTitle.FromString("Test ad"));
            _jobAd.UpdateSalary(
                Salary.FromDecimal(100.10m, "EUR", new FakeCurrencyLookup()));

            Assert.Throws<InvalidEntityStateException>(() => _jobAd.RequestToPublish());
        }

        [Fact]
        public void Cannot_publish_without_salary()
        {
            _jobAd.SetTitle(JobAdTitle.FromString("Test ad"));
            _jobAd.UpdateText(JobAdText.FromString("Job Description"));

            Assert.Throws<InvalidEntityStateException>(() => _jobAd.RequestToPublish());
        }

        [Fact]
        public void Cannot_publish_with_zero_salary()
        {
            _jobAd.SetTitle(JobAdTitle.FromString("Test ad"));
            _jobAd.UpdateText(JobAdText.FromString("Job Description"));
            _jobAd.UpdateSalary(
                Salary.FromDecimal(0.0m, "EUR", new FakeCurrencyLookup()));

            Assert.Throws<InvalidEntityStateException>(() => _jobAd.RequestToPublish());
        }
    }
}
