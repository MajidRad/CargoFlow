using System;
using System.Collections.Generic;
using System.Text;

namespace CargoFlow.Identity.Domain.Exceptions;

public sealed class InvalidEmailException:Exception
{
    public string Email { get;  }
    public InvalidEmailException(string email)
        :base($"Invalid email format:'{email}'")
    {
        Email = email;
    }
}
