using ECommerce.Application.DTOs.Customers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Customers.Queries.GetCustomerById
{
    public sealed record GetCustomerByIdQuery(int Id) : IRequest<CustomerResponse>
    {
    }
}
