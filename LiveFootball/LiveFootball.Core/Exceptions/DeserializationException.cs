/**************************************************************************
 *                                                                        * 
 *  File:        ResultsDeserializer.cs                                   *
 *  Description: ApiFootballDeserializer Library                          *
 *               Class for deserializing result match data from JSON.     *
 *               Implements the IResultsDeserializer interface.           *
 *  Copyright:   (c) 2024, LiveFootball Team                              *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/
namespace LiveFootball.Core.Exceptions
{
    /// <summary>
    /// Exception thrown when there is an error during deserialization.
    /// </summary>
    public class DeserializationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeserializationException"/> class.
        /// </summary>
        public DeserializationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeserializationException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public DeserializationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeserializationException"/> class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public DeserializationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
