using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobMarket.Domain;
using JobMarket.Library;
using static JobMarket.Contracts.JobAds;

namespace JobMarket.API
{
    public class JobAdsApplicationService : IApplicationService
    {
        private readonly IJobAdRepository _repository;
        private readonly ICurrencyLookup _currencyLookup;

        public JobAdsApplicationService()
        {
        }

        public JobAdsApplicationService(
            IJobAdRepository repository,
            ICurrencyLookup currencyLookup
        )
        {
            _repository = repository;
            _currencyLookup = currencyLookup;
        }

        public Task Handle(object command) =>
            command switch
            {
                V1.Create cmd =>
                    HandleCreate(cmd),
                V1.SetTitle cmd =>
                    HandleUpdate(
                        cmd.Id,
                        c => c.SetTitle(
                            JobAdTitle.FromString(cmd.Title)
                        )
                    ),
                V1.UpdateText cmd =>
                    HandleUpdate(
                        cmd.Id,
                        c => c.UpdateText(
                            JobAdText.FromString(cmd.Text)
                        )
                    ),
                V1.UpdateSalary cmd =>
                    HandleUpdate(
                        cmd.Id,
                        c => c.UpdateSalary(
                            Salary.FromDecimal(
                                cmd.Salary,
                                cmd.Currency,
                                _currencyLookup
                            )
                        )
                    ),
                V1.RequestToPublish cmd =>
                    HandleUpdate(
                        cmd.Id,
                        c => c.RequestToPublish()
                    ),
                _ => Task.CompletedTask
            };

        private async Task HandleCreate(V1.Create cmd)
        {
            if (await _repository.Exists(cmd.Id.ToString()))
                throw new InvalidOperationException($"Entity with id {cmd.Id} already exists");

            var jobAd = new JobAd(
                new JobAdId(cmd.Id),
                new UserId(cmd.OwnerId)
            );

            await _repository.Save(jobAd);
        }

        private async Task HandleUpdate(
            Guid jobAdId,
            Action<JobAd> operation
        )
        {
            var jobAd = await _repository.Load(
                jobAdId.ToString()
            );
            if (jobAd == null)
                throw new InvalidOperationException(
                    $"Entity with id {jobAdId} cannot be found"
                );

            operation(jobAd);

            await _repository.Save(jobAd);
        }
    }
}
