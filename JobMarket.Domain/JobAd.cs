using System;
using System.Collections.Generic;
using System.Text;
using JobMarket.Library;

namespace JobMarket.Domain
{
    public class JobAd : Entity<JobAdId>
    {
        public JobAdId Id { get; private set; }
        public UserId OwnerId { get; private set; }
        public JobAdTitle Title { get; private set; }
        public JobAdText Text { get; private set; }
        public Salary Salary { get; private set; }
        public JobAdState State { get; private set; }
        public UserId ApprovedBy { get; private set; }

        public JobAd(JobAdId id, UserId ownerId) =>
            Apply(new Events.JobAdCreated
            {
                Id = id,
                OwnerId = ownerId
            });

        public void SetTitle(JobAdTitle title) =>
            Apply(new Events.JobAdTitleChanged
            {
                Id = Id,
                Title = title
            });

        public void UpdateText(JobAdText text) =>
            Apply(new Events.JobAdTextUpdated
            {
                Id = Id,
                AdText = text
            });

        public void UpdateSalary(Salary salary) =>
            Apply(new Events.JobAdSalaryUpdated
            {
                Id = Id,
                Salary = salary.Amount,
                CurrencyCode = salary.Currency.CurrencyCode
            });

        public void RequestToPublish() =>
            Apply(new Events.JobAdSentForReview { Id = Id });

        protected override void When(object @event)
        {
            switch (@event)
            {
                case Events.JobAdCreated e:
                    Id = new JobAdId(e.Id);
                    OwnerId = new UserId(e.OwnerId);
                    State = JobAdState.Inactive;
                    break;
                case Events.JobAdTitleChanged e:
                    Title = new JobAdTitle(e.Title);
                    break;
                case Events.JobAdTextUpdated e:
                    Text = new JobAdText(e.AdText);
                    break;
                case Events.JobAdSalaryUpdated e:
                    Salary = new Salary(e.Salary, e.CurrencyCode);
                    break;
                case Events.JobAdSentForReview e:
                    State = JobAdState.PendingReview;
                    break;
            }
        }

        protected override void EnsureValidState()
        {
            var valid =
                Id != null &&
                OwnerId != null &&
                (State switch
                {
                    JobAdState.PendingReview =>
                        Title != null
                        && Text != null
                        && Salary?.Amount > 0,
                    JobAdState.Active =>
                        Title != null
                        && Text != null
                        && Salary?.Amount > 0
                        && ApprovedBy != null,
                    _ => true
                });

            if (!valid)
                throw new InvalidEntityStateException(
                    this, $"Post-checks failed in state {State}");
        }

        public enum JobAdState
        {
            PendingReview,
            Active,
            Inactive,
            MarkedAsFulfilled
        }
    }
}
