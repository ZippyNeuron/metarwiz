using System;

namespace ZippyNeuron.Metarwiz
{
    public class MetarException : ApplicationException
    {
        public MetarException(string message) : base(message)
        {

        }

        public MetarException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}