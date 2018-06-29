using System;

namespace DNAAnalyzer.NET.Exceptions
{
    [Serializable]
    public class InvalidJSONException : Exception
    {
        public InvalidJSONException() : base()
        {
        }

        public InvalidJSONException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
