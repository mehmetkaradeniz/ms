using AspnetRunBasics.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<CatalogModel>> GetAll();
        Task<IEnumerable<CatalogModel>> GetByCategory(string category);
        Task<CatalogModel> GetById(string id);
        Task<CatalogModel> Create(CatalogModel model);
    }
}
