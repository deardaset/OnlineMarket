using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.SharedKernel.Contracts.Contracts.Requests.Product
{
    public class GetAllProductsParametersRequest
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string? Sort { get; set; }
        public string? Search { get; set; }
        public string? Category { get; set; }
    }
}
