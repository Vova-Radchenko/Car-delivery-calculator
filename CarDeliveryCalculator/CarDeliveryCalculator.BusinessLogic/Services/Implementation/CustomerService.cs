using System;
using CarDeliveryCalculator.BusinessLogic.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarDeliveryCalculator.DataAccess.Entities;
using CarDeliveryCalculator.DataAccess.Repositories.Interfaces;
namespace CarDeliveryCalculator.BusinessLogic.Services.Implementation
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            this._repository = repository;
        }

        public async Task AddAsync(Customer customer) 
            => await _repository.AddAsync(customer);

        public async Task DeleteAsync(Customer customer) 
            => await _repository.DeleteAsync(customer);

        public async Task<ICollection<Customer>> GetAllAsync() 
            => await _repository.GetAllAsync();

        public async Task<Customer> GetByIdAsync(int id) 
            => await _repository.GetByIdAsync(id);

        public async Task<bool> TryUpdateAsync(int id, Customer customer)
        {
            var customerToUpdate = await this._repository.GetByIdAsync(id);
            if(customerToUpdate != null)
            {
                customerToUpdate.FirstName = customer.FirstName;
                customerToUpdate.LastName = customer.LastName;
                customerToUpdate.EMail = customer.EMail;
                customerToUpdate.PhoneNumber = customer.PhoneNumber;

                await this._repository.UpdateAsync(customerToUpdate);

                return true;
            }

            return false;
        }
    }
}