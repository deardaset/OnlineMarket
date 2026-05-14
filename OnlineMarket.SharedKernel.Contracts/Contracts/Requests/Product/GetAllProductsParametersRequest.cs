using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.SharedKernel.Contracts.Contracts.Requests.Product
{
    public class GetAllProductsParametersRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? Sort { get; set; }
        public string? Search { get; set; }
        public string? Category { get; set; }
    }
}
