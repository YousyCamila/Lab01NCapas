using DAL;
using Entities.Models;

namespace BLL
{
    public class Customers
    {
        public async Task<Customer> CreateAsync(Customer customer)
        {
            Customer customerResult = null;
            using (var repository = RepositoryFactory.CreateRepository())
            {
                // Buscar si el nombre de cliente existe
                Customer customerSearch = await repository.RetrieveAsync<Customer>(c => c.FirstName == customer.FirstName);
                if (customerSearch == null)
                {
                    // No existe, podemos crearlo
                    customerResult = await repository.CreateAsync(customer);
                }
                else
                {
                    // Podríamos aquí lanzar una excepción
                    // para notificar que el Cliente ya existe.
                    // Podríamos incluso crear una capa de Excepciones
                    // personalizadas y consumirla desde otras
                    // capas.
                    CustomerExceptions.ThrowCustomerAlreadyExitsException(customerSearch.FirstName, customerSearch.LastName);
                }
            }
            return customerResult!;
        }
    }
}
