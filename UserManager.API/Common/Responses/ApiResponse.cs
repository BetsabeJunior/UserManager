// <copyright file="ApiResponse.cs" company="PlaceholderCompany">
// Copyright (c) DITOS SAS. All rights reserved.
// </copyright>

namespace UserManager.API.Common.Responses
{
    /// <summary>
    /// Standard response format for API.
    /// </summary>
    /// <typeparam name="T">Type of data to return.</typeparam>
    public class ApiResponse<T>
    {
        /// <summary>
        /// Indicates if the response is success or error.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Message about the response.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Data returned (can be null on error).
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Create a success response.
        /// </summary>
        /// <param name="data">Data to send.</param>
        /// <param name="message">Optional message.</param>
        /// <returns>ApiResponse with success true.</returns>
        public static ApiResponse<T> Ok(T data, string message = "")
        {
            return new ApiResponse<T> { Success = true, Message = message, Data = data };
        }

        /// <summary>
        /// Create a fail response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>ApiResponse with success false.</returns>
        public static ApiResponse<T> Fail(string message)
        {
            return new ApiResponse<T> { Success = false, Message = message, Data = default };
        }
    }
}
