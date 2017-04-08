/* -------------------------------------------------------------------
*   Copyright © 2017 While False Studios
*   AssemblyError.cs created by Andrew on 2017-4-7 
*   Purpose: exception for the assembly engine
--------------------------------------------------------------------- */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhileFalseStudios.Assembly
{
    /// <summary>
    /// Exception thrown when the assembly interpreter encounters an error.
    /// </summary>
    public class AssemblyException : Exception
    {
        /// <summary>
        /// The type of exception thrown.
        /// </summary>
        public enum ExceptionType
        {
            /// <summary>
            /// A syntax error occurred during tokenisation.
            /// </summary>
            Syntax,
            /// <summary>
            /// A runtime error occurred during execution.
            /// </summary>
            Runtime,
            /// <summary>
            /// A parser error occurred. This may happen when a program is corrupt.
            /// </summary>
            Parser
        }

        string userMessage;
        ExceptionType type;
        uint lineNumber;

        /// <summary>
        /// Constructs a new assembly exception.
        /// </summary>
        /// <param name="msg">The message that is shown in the exception.</param>
        /// <param name="t">The type of exception thrown.</param>
        /// <param name="line">The assembly source line number the exception was thrown from.</param>
        public AssemblyException(string msg, ExceptionType t, uint line)
        {
            userMessage = msg;
            type = t;
            lineNumber = line;
        }

        /// <summary>
        /// The type of error that occurred.
        /// </summary>
        public ExceptionType Type
        {
            get
            {
                return type;
            }
        }

        /// <summary>
        /// The line in the assembly source code the error occurred on.
        /// </summary>
        public uint LineNumber
        {
            get
            {
                return lineNumber;
            }
        }

        /// <summary>
        /// Formatted error message.
        /// </summary>
        public override string Message
        {
            get
            {
                return string.Format("{0} error on line {1}: {2}", Enum.GetName(typeof(ExceptionType), type), lineNumber, userMessage);
            }
        }
    }
}
