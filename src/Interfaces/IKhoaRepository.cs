using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Interfaces
{
    public interface IKhoaRepository
    {
        public Task<List<Khoa>> GetListKhoa();
        public Task<Khoa?> CreateKhoa(Khoa khoa);
        public Task<int> CheckKhoa(string tenKhoa);
        public Task<string?> GetMaKhoa(int id);
        public Task<Khoa?> GetKhoaId(int id);
        
    }
}