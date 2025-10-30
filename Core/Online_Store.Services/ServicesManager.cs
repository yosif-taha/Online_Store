using AutoMapper;
using Online_Store.Domain.Contracts;
using Online_Store.Services.Abstractions;
using Online_Store.Services.Abstractions.Product;
using Online_Store.Services.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Services
{
    public class ServicesManager(IUnitOfWork _unitOfWork , IMapper _mapper) : IServicesManager
    {
        public IProductService ProductService { get; } = new ProductService(_unitOfWork, _mapper);
    }
}
