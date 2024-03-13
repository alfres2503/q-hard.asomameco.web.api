﻿using src.Models;
using src.Repository;

namespace src.Services
{
    public class CateringServiceService : ICateringServiceService
    {
        private readonly ICateringServiceRepository _cateringserviceRepository;

        public CateringServiceService(ICateringServiceRepository cateringserviceRepository)
        {
            _cateringserviceRepository = cateringserviceRepository;
        }

        public async Task<IEnumerable<CateringService>> GetAll(int pageNumber, int pageSize)
        {
            try { return await _cateringserviceRepository.GetAll(pageNumber, pageSize).ConfigureAwait(false); }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
            
        }

        public async Task<int> GetCount()
        {
            try { return await _cateringserviceRepository.GetCount().ConfigureAwait(false); }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
        }

        public async Task<CateringService> GetByEmail(string email)
        {
            try { return await _cateringserviceRepository.GetByEmail(email).ConfigureAwait(false); }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
        }

        public async Task<CateringService> GetByID(int id)
        {
            try { return await _cateringserviceRepository.GetByID(id).ConfigureAwait(false); }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
        }
        public async Task<CateringService> Create(CateringService catering_service)
        {
            try { return await _cateringserviceRepository.Create(catering_service).ConfigureAwait(false); }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
        }

        public async Task<CateringService> Update(CateringService catering_service)
        {
            try { return await _cateringserviceRepository.Update(catering_service); }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try { return await _cateringserviceRepository.Delete(id); }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
        }

        public async Task<CateringService> ChangeState(int id)
        {
            try { return await _cateringserviceRepository.ChangeState(id); }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
        }

    }
}
