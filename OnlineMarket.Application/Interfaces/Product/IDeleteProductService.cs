using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Application.Interfaces.Product
{
    public interface IDeleteProductService
    {
        public Task RunAsync(Guid guid);
    }
}
