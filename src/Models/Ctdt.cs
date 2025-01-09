using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Result_Management_System.Models;

public class Ctdt
{
    public int Id { get; set; }
    public int NganhId { get; set; }
    public int HocPhanId { get; set; }
    public bool LaCotLoi { get; set; }
}
