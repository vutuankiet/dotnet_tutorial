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
    public class OrderDetailDAO : BaseDAO, IOderDetailDAO
    {
        public async Task<bool> Add(OrderDetail entity)
        {
            try
            {
                _context.OrderDetails.Add(entity);

                //su dung savechangeAsyn thi phai co await, tra ve se bat dong bbo vi cos nhieu asyn

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                //lay doi tuong trong danh sach
                OrderDetail entity = await GetSingleByID(id);
                _context.OrderDetails.Remove(entity);
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

            return false;
        }

        public async Task<bool> DeleteByOrderDeltaiID(int orderID)
        {
            try
            {
                //lay doi tuong trong danh sach
                OrderDetail orderDetails = await GetSingleByID(orderID);
                _context.OrderDetails.Remove(orderDetails);
                //su dung remove de xoa
                //su dung savechangeAsyn thi phai co await, tra ve se bat dong bbo vi cos nhieu asyn
                //await la 1 len chờ Asyn thuc thi

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                
            }

            return false;
        }

        public async Task<List<OrderDetail>> GetAll()
        {
            List<OrderDetail> orderDetails = await _context.OrderDetails.ToListAsync();

            return orderDetails;
        }

        public Task<List<OrderDetail>> GetByKeyword(string keyword)
        {
            throw new NotImplementedException();
        }

        public async Task<List<OrderDetail>> GetByOrderID(int orderID)
        {
            List<OrderDetail> orderDetails = await _context.OrderDetails
                .Where(t => t.OrderID == orderID)
                .ToListAsync();

            return orderDetails;
        }

        public async Task<IPagedList<OrderDetail>> GetByPaged(int page, int pageSize, string keyword)
        {
            List<OrderDetail> orderDetails = await GetByKeyword(keyword);

            IPagedList<OrderDetail> pageListOrderDetail = orderDetails.ToPagedList(page, pageSize);

            return pageListOrderDetail;
        }

        public async Task<OrderDetail> GetSingleByID(int id)
        {
            OrderDetail orderDetail = await _context.OrderDetails.FindAsync(id);

            return orderDetail;
        }

        public async Task<bool> Update(OrderDetail entity)
        {
            try
            {
                OrderDetail cOrderDetails = await GetSingleByID(entity.ID);

                _context.OrderDetails.Add(entity);

                //su dung savechangeAsyn thi phai co await, tra ve se bat dong bbo vi cos nhieu asyn
                if (cOrderDetails != null)
                {
                    cOrderDetails.UnitPrice = entity.UnitPrice;
                    cOrderDetails.Quantity = entity.Quantity;

                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
        }
    }
}