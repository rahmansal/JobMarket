using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobMarket.Library
{
    public interface IApplicationService
    {
        Task Handle(object command);
    }
}
