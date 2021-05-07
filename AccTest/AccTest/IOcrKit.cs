using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AccTest
{
    public interface IOcrKit
    {
        Task<string> ExtractText(Stream image);
    }
}
