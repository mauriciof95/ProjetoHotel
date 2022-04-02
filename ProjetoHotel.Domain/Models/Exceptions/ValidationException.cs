using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoHotel.Domain.Models.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string msg) : base(msg) { }
    }
}
