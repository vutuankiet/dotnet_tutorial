using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSales.Models.EF;

namespace WebSales.Models.DAO
{
    //tra ve 1 danh sach ipagelist
    public interface ICustomerDAO : IBaseDAO<Customer, string>, IPagedListDAO<Customer>

    {

    }
}