using Application.Services.Interfaces;
using Domain.Models.Domain;
using Domain.Response;
using Persistance.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Services
{
    public class AdressServices : IAdressServices
    {
        private readonly IAdressRepository _adressRepository;
        public AdressServices(IAdressRepository adressRepository)
        {
            _adressRepository = adressRepository;
        }
        public async Task<ServiceResponse> AddAsync(Addresses addresses)
        {
            await _adressRepository.AddAsync(addresses);
            return new ServiceResponse(true, "Dodano nowy adres");
        }

        public async Task<ServiceResponse> DeleteAsync(Addresses addresses)
        {
            await _adressRepository.DeleteAsync(addresses);
            return new ServiceResponse(true, "Usunięto adres");
        }

        public async Task<ServiceResponse<List<Addresses>>> GetAllAsync()
        {
            var adressess = await _adressRepository.GetAllAsync();
            if (adressess == null || adressess.Count == 0)
            {
                return new ServiceResponse<List<Addresses>>(false, "Brak adresów w bazie danych");
            }
            return new ServiceResponse<List<Addresses>>(adressess, true);
        }

        public async Task<ServiceResponse<Addresses>> GetByIdAsync(int id)
        {
            var adress = await _adressRepository.GetByIdAsync(id);
            if (adress == null)
            {
                return new ServiceResponse<Addresses>(false, "Brak adresu o podanym Id");
            }
            return new ServiceResponse<Addresses>(adress, true);
        }

        public async Task<ServiceResponse> UpdateAsync(Addresses addresses, int id)
        {
            var adressFromBase = await _adressRepository.GetByIdAsync(id);
            if (adressFromBase == null)
            {
                return new ServiceResponse(false, "Brak adresu o takim Id w bazie danych");
            }
            adressFromBase.Street = addresses.Street;
            adressFromBase.City = addresses.City;
            adressFromBase.PostalCode = addresses.PostalCode;

            await _adressRepository.UpdateAsync(adressFromBase);

            return new ServiceResponse(true, "Zaktualizowano adres");
        }
    }
}
