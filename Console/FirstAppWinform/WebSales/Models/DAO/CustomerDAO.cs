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
    public class CustomerDAO : BaseDAO, ICustomerDAO
    {
        public async Task<bool> Add(Customer entity)
        {
            //them sua xoa thi nen them try catch de tim loi,th nay neu co loi return ve false
            //Định nghĩa cho chức năng Add của Product
            try
            {
                _context.Customers.Add(entity);

                //su dung savechangeAsyn thi phai co await, tra ve se bat dong bbo vi cos nhieu asyn

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
        }

        public async Task<bool> Delete(string id)
        {
            try
            {
                //lay doi tuong trong danh sach
                Customer entity = await GetSingleByID(id);
                _context.Customers.Remove(entity);
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

        public async Task<List<Customer>> GetAll()
        {
            List<Customer> customers = await _context.Customers.ToListAsync();

            return customers;
        }

        public async Task<List<Customer>> GetByKeyword(string keyword)
        {
            List<Customer> customers = await _context.Customers
                .Where(t => t.ID.Contains(keyword))
                .ToListAsync();

            return customers;
        }

        public async Task<IPagedList<Customer>> GetByPaged(int page, int pageSize, string keyword)
        {
            List<Customer> customers = await GetByKeyword(keyword);

            IPagedList<Customer> pageListCustomers = customers.ToPagedList(page, pageSize);

            return pageListCustomers;
        }

        public async Task<Customer> GetSingleByID(string id)
        {
            Customer customer = await _context.Customers.FindAsync(id);

            return customer;
        }

        public async Task<bool> Update(Customer entity)
        {
            try
            {
                Customer cEntity = await GetSingleByID(entity.ID);

                _context.Customers.Add(entity);

                //su dung savechangeAsyn thi phai co await, tra ve se bat dong bbo vi cos nhieu asyn
                if (cEntity != null)
                {
                    cEntity.Password = entity.Password;
                    cEntity.Fullname = entity.Fullname;
                    cEntity.Email = entity.Email;
                    cEntity.Photo = entity.Photo;
                    cEntity.Activated = entity.Activated;

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