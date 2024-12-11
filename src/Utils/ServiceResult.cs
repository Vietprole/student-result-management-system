using System;

namespace Student_Result_Management_System.Utils;

public class ServiceResult<T>
{
    public bool IsSuccess { get; private set; }
    public string? ErrorMessage { get; private set; }
    public T? Data { get; private set; }

    public static ServiceResult<T> Success(T data) => 
        new() { IsSuccess = true, Data = data };
    
    public static ServiceResult<T> Failure(string error) => 
        new() { IsSuccess = false, ErrorMessage = error };
}
