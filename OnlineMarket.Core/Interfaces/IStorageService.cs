using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Core.Interfaces
{
    public interface IStorageService
    {
        public Task<string> UploadAsync(Stream stream, string fileName, string contentType);
        public Task DeleteAsync(string fileUrl);
    }
}
