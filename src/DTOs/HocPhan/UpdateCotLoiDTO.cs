using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Result_Management_System.DTOs.HocPhan;

public class UpdateCotLoiDTO
{
    public int HocPhanId { get; set; }
    public bool LaCotLoi { get; set; }
}
