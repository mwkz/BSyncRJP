using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Common.Core.Services
{    
    public record ValidationData(string Message, string PropertyName, ValidationSeverity Severity);
}
