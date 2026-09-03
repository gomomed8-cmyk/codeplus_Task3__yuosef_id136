using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Interfaces.Services
{
    public interface IBasketExpirationService
    {
        Task ProcessAsync(
        CancellationToken cancellationToken = default);
    }
}
