namespace PandaPeUtilidades.Exceptions
{
    using System;

    /// <summary>
    /// Base exception class for custom exceptions in the PandaPe
    /// </summary>
    /// <remarks>
    ///     Author: Juan David Parroquiano 
    ///     Date: 08/ Octubre / 2023
    /// </remarks>
    public class Base : Exception
    {
        /// <summary>
        /// Initializes a new instance
        /// </summary>
        public Base() : base() { }

        /// <summary>
        /// Initializes a new instance class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public Base(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance class with a specified error message
        /// and a reference to the inner exception that caused the current exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public Base(string message, Exception innerException) : base(message, innerException) { }
    }
}

