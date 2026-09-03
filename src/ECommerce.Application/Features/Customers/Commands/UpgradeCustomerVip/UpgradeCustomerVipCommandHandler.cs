using ECommerce.Application.Interfaces.Services;
using MediatR;


namespace ECommerce.Application.Features.Customers.Commands.UpgradeCustomerVip
{
    public sealed class UpgradeCustomerVipCommandHandler : IRequestHandler<UpgradeCustomerVipCommand, Unit>
    {
        private readonly ICustomerService _customerService;
        public UpgradeCustomerVipCommandHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<Unit> Handle(UpgradeCustomerVipCommand request, CancellationToken cancellationToken)
        {
         await _customerService.UpgradeToVipAsync(request.CustomerId, cancellationToken);
            return Unit.Value;
        }
    }
}
