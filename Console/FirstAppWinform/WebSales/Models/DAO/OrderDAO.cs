using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebSales.Models.EF;

namespace WebSales.Models.DAO
{
    public class OrderDAO : BaseDAO, IOrderDAO
    {
        public async Task<bool> Add(Order entity)
        {
            try
            {
                _context.Orders.Add(entity);

                //su dung savechangeAsyn thi phai co await, tra ve se bat dong bbo vi cos nhieu asyn

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                //lay doi tuong trong danh sach
                Order entity = await GetSingleByID(id);
                _context.Orders.Remove(entity);
                //su dung remove de xoa
                //su dung savechangeAsyn thi phai co await, tra ve se bat dong bbo vi cos nhieu asyn
                //await la 1 len chờ Asyn thuc thi

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
                //retunr se tra ve task ko phai du lieu
            }

            return true;
        }

        public async Task<List<Order>> GetAll()
        {
            List<Order> orders = await _context.Orders.ToListAsync();

            return orders;
        }

        public async Task<List<Order>> GetByKeyword(string keyword)
        {
            List<Order> orders = await _context.Orders
                .Where(t => t.CustomerID.Contains(keyword))
                .ToListAsync();

            return orders;
        }

        public async Task<IPagedList<Order>> GetByPaged(int page, int pageSize, string keyword)
        {
            List<Order> orders = await GetByKeyword(keyword);

            IPagedList<Order> pageListOrders = orders.ToPagedList(page, pageSize);

            return pageListOrders;
        }

        public async Task<Order> GetSingleByID(int id)
        {
            Order order = await _context.Orders.FindAsync(id);

            return order;
        }

        public async Task<bool> Update(Order entity)
        {
            try
            {
                Order cOrder = await GetSingleByID(entity.ID);

                _context.Orders.Add(entity);

                //su dung savechangeAsyn thi phai co await, tra ve se bat dong bbo vi cos nhieu asyn
                if (cOrder != null)
                {
                    cOrder.Address = entity.Address;
                    cOrder.Amount = entity.Amount;
                    cOrder.Description = entity.Description;

                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}