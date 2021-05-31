using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobMarket.Domain
{
    public interface IJobAdRepository
    {
        Task<bool> Exists(JobAdId id);

        Task<JobAd> Load(JobAdId id);

        Task Save(JobAd entity);
    }
}
