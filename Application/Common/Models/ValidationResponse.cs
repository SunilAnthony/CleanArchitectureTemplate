using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class ValidationResponse
    {
        public List<ValidationError> Errors { get; set; } = new List<ValidationError>();
    }
}
