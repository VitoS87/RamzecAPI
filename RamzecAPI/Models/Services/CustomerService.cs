using RamzecAPI.Models.Abstractions;
using RamzecAPI.Models.Data;
using static RamzecAPI.Models.Abstractions.ICommonService;

namespace RamzecAPI.Models.Services
{
    public class CustomerService : AbstractionService, ICommonService<Customer, int>
    {
        private int pageSize = 10;
        private readonly AppDbContext db;
        public CustomerService(AppDbContext _db)
        {
            this.db = _db;
        }
        public bool Create(Customer model)
        {
            bool result = DoAction(delegate ()
            {
                db.Customers.Add(model);
                db.SaveChangesAsync();
            });
            return result;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Customer>> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(Customer model)
        {
            throw new NotImplementedException();
        }
    }
}
