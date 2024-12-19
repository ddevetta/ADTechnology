using System;

namespace ADTechnology.AbInitio.Classes
{
    internal class NotAnAILogException : ApplicationException
    {
        public int LineNumber;

        public NotAnAILogException(int lineNumber)
          : base("The log file was not recognised as a valid Ab Initio log.")
        {
            this.LineNumber = lineNumber;
        }
    }
}
