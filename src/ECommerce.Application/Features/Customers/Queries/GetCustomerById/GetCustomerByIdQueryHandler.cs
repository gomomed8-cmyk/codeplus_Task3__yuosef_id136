using ECommerce.Application.DTOs.Customers;
using ECommerce.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Customers.Queries.GetCustomerById
{
    public sealed class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerResponse>
    {
        private readonly ICustomerService _customerService;
        public GetCustomerByIdQueryHandler(ICustomerService customerService)
        { 
        _customerService = customerService;
        }

        public async Task<CustomerResponse> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
           return await _customerService.GetCustomerByIdAsync(request.Id, cancellationToken);
        }
    }
}

