using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebSales.Models.EF;

namespace WebSales.Models.DAO
{
    public interface IProductDAO : IBaseDAO<Product, int>, IPagedListDAO<Product>
    {
        Task<Customer> GetSingleByID(string id);
    }
}