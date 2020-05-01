using System;

namespace Persistance.repositories
{
    public class PersistanceException : Exception
    {
        public PersistanceException(string message) : base(message)
        {
        }
    }
}