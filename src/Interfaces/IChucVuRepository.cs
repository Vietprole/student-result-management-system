using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student_Result_Management_System.Interfaces
{
    public interface IChucVuRepository
    {
        public Task<string?> GetIdChucVu(string chucvu);
        public Task<List<string>> GetListChucVu();
    }
}