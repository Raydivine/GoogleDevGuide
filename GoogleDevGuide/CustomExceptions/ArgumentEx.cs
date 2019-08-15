using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleDevGuide.CustomExceptions
{
    public class NumberIsNotEvenException : Exception
    {
        public NumberIsNotEvenException()
        {
        }

        public NumberIsNotEvenException(string message)
            : base(message)
        {
        }

        public NumberIsNotEvenException(int number)
            : base($"The length is {number}")
        {
        }

        public NumberIsNotEvenException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
