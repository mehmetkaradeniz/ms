using Shopping.Aggregator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Services
{
    public interface ICatalogService
    {
        Task<ICollection<CatalogModel>> GetAll();
        Task<CatalogModel> GetById(string id);
        Task<ICollection<CatalogModel>> GetByCategory(string category);
    }
}
