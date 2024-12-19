using System;

namespace ADTechnology.AbInitio.Classes
{
    internal class EmptyLogException : ApplicationException
    {
        public int LineNumber;

        public EmptyLogException(int lineNumber)
          : base("The log file does not have any executed components (vertexes).")
        {
            this.LineNumber = lineNumber;
        }
    }
}
