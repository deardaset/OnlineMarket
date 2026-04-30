using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.SharedKernel.Contracts.Contracts.Responses
{
    public class PagedResponse<T>
    {
        public List<T> Items { get; set; } = null!;
        public int TotalCount { get; set; }
    }
}
