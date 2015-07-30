using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.DAL
{
    public class NtException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the Exception class.
        /// </summary>
        public NtException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Exception class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public NtException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Exception class with a specified error message.
        /// </summary>
		/// <param name="messageFormat">The exception message format.</param>
		/// <param name="args">The exception message arguments.</param>
        public NtException(string messageFormat, params object[] args)
			: base(string.Format(messageFormat, args))
		{
		}

    }




}
