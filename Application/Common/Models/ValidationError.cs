using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public sealed class ValidationError
    {
        public string FieldName { get; set; }
        public string Message { get; set; }
    }
}
