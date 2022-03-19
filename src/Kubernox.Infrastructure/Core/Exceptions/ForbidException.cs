using System;

namespace Kubernox.Infrastructure.Core.Exceptions
{
    public class ForbidException : Exception
    {
        public ForbidException()
        {
        }

        public ForbidException(string message)
            : base(message)
        {
        }
    }
}
