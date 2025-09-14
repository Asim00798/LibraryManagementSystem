﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.DTOs.Security
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; } = true;
        public string? Message { get; set; }
        public T? Data { get; set; }

        public static ApiResponse<T> SuccessResponse(T data, string? message = null)
            => new() { Success = true, Data = data, Message = message };

        public static ApiResponse<T> FailureResponse(string message)
            => new() { Success = false, Message = message };
    }

}
