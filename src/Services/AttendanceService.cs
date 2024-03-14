using src.Models;
using src.Repository;

namespace src.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;

        public AttendanceService(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        public async Task<IEnumerable<Attendance>> GetAll(int pageNumber, int pageSize)
        {
            try { return await _attendanceRepository.GetAll(pageNumber, pageSize).ConfigureAwait(false); }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
            
        }

        public async Task<int> GetCount()
        {
            try { return await _attendanceRepository.GetCount().ConfigureAwait(false); }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<Attendance>> GetByIdEvent(int id, int pageNumber, int pageSize)
        {
            try { return await _attendanceRepository.GetByIdEvent(id, pageNumber, pageSize).ConfigureAwait(false); }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<Attendance>> GetByIdAssociate(int id, int pageNumber, int pageSize)
        {
            try { return await _attendanceRepository.GetByIdAssociate(id, pageNumber, pageSize).ConfigureAwait(false); }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
        }

        public async Task<Attendance> GetByIdEventIdAssociate(int idEvent, int idAssociate)
        {
            try { return await _attendanceRepository.GetByIdEventIdAssociate(idEvent, idAssociate).ConfigureAwait(false); }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
        }

        public async Task<Attendance> Create(Attendance attendance)
        {
            try { return await _attendanceRepository.Create(attendance).ConfigureAwait(false); }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
        }

    }
}
