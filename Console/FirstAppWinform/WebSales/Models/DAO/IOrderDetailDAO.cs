using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebSales.Models.EF;

namespace WebSales.Models.DAO
{
    public interface IOderDetailDAO : IBaseDAO<OrderDetail, int>, IPagedListDAO<OrderDetail>
    {
        Task<List<OrderDetail>> GetByOrderID(int orderID);
        Task<bool> DeleteByOrderDeltaiID(int orderID);
    }
}