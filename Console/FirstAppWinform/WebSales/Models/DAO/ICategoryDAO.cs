using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSales.Models.EF;

namespace WebSales.Models.DAO
{
    //xac dinh interface cac chuc nang cho ICategoryDAO
    //sau đó các bạn vào các interface của các đối tượng và thêm int vàotrừ customer là string
    public interface ICategoryDAO : IBaseDAO<Category, int>, IPagedListDAO<Category>
    {
    }
}