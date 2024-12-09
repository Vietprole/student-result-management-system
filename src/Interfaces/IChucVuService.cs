namespace Student_Result_Management_System.Interfaces
{
    public interface IChucVuService
    {
        public Task<string?> GetIdChucVu(string chucvu);
        public Task<List<string>> GetListChucVu();
    }
}