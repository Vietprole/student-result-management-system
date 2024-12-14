using System;

namespace Student_Result_Management_System.Utils;

public class NotFoundException(string message) : Exception(message)
{

}
