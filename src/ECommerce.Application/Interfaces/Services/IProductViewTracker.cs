using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Interfaces.Services
{
    public interface IProductViewTracker
    {
        Task IncrementAsync(int productId);
        Task<int> GetViewCountAsync(int productId);
        Task<long> GetAndResetAsync(int productId);

    }
}
