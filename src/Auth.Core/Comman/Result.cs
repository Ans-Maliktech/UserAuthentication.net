using System;
using System.Collections.Generic;

namespace Auth.Core.Common
{
    public class Result<T>
    {
        public T Value { get; }
        public bool IsSuccess { get; }
        
        // --- FIX 1: Add 'IsFailed' property ---
        public bool IsFailed => !IsSuccess; 

        public string? Error { get; } // Added '?' to fix Null Warning

        // --- FIX 2: Add 'Errors' property ---
        // Converts the single error string into a list/array so your Services are happy
        public IEnumerable<string> Errors => string.IsNullOrEmpty(Error) 
            ? Array.Empty<string>() 
            : new[] { Error };

        protected Result(T value, bool isSuccess, string? error)
        {
            Value = value;
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result<T> Success(T value) => new Result<T>(value, true, null);
        
        // Use 'default!' to fix the "Cannot convert null" warning
        public static Result<T> Failure(string error) => new Result<T>(default!, false, error);
    }
}