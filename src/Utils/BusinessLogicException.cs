using System;

namespace Student_Result_Management_System.Utils;

public class BusinessLogicException(string message) : Exception(message)
{
}
