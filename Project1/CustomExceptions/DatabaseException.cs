using System;

namespace Project1.CustomExceptions
{
    public class DatabaseException : Exception
    {

        public DatabaseException ( string message, Exception exception )
            : base ( message, exception )
        {

        }

    }
}
