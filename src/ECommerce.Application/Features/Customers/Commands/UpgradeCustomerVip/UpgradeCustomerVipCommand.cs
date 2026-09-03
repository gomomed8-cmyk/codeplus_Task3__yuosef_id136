using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Customers.Commands.UpgradeCustomerVip
{
    public sealed record UpgradeCustomerVipCommand(int CustomerId) : IRequest<Unit>
    {
    }
}
