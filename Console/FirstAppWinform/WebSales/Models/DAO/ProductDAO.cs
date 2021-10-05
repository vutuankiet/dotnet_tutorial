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
    //khi khai bao chuc nang chi can khai bao den interface tu dong ren code implement nhanh tien loi
    //Tạo Class Product kế thừa BaseDAO, IProductDAO
    //interface chi khai bao phuong thuc mac dinh
    public class ProductDAO : BaseDAO, IProductDAO
    {
        public async Task<bool> Add(Product entity)
        {
            //them sua xoa thi nen them try catch de tim loi,th nay neu co loi return ve false
            //Định nghĩa cho chức năng Add của Product
            try
            {
                _context.Products.Add(entity);

                //su dung savechangeAsyn thi phai co await, tra ve se bat dong bbo vi cos nhieu asyn

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
        }

        //Định nghĩa cho chức năng Delete của Product
        //xoa theo ID
        public async Task<bool> Delete(int id)
        {
            try
            {
                //lay doi tuong trong danh sach
                Product entity = await GetSingleByID(id);
                _context.Products.Remove(entity);
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

        //Định nghĩa cho chức năng GetAll của Product
        public async Task<List<Product>> GetAll()
        {
            List<Product> products = await _context.Products.ToListAsync();

            return products;
        }

        //tim kiem thep ten
        //Định nghĩa cho chức năng GetByKeyword của Product
        public async Task<List<Product>> GetByKeyword(string keyword)
        {
            List<Product> products = await _context.Products
                .Where(t => t.Name.Contains(keyword))
                .ToListAsync();

            return products;
        }

        //Định nghĩa cho chức năng GetByPaged của Product
        public async Task<IPagedList<Product>> GetByPaged(int page, int pageSize, string keyword)
        {
            List<Product> products = await GetByKeyword(keyword);

            IPagedList<Product> pageListProducts = products.ToPagedList(page, pageSize);

            return pageListProducts;
        }

        //Định nghĩa cho chức năng GetSingleByID của Product
        public async Task<Product> GetSingleByID(int id)
        {
            Product product = await _context.Products.FindAsync(id);

            return product;
        }

        public Task<Customer> GetSingleByID(string id)
        {
            throw new NotImplementedException();
        }

        //Định nghĩa cho chức năng Update của Product
        public async Task<bool> Update(Product entity)
        {
            try
            {
                Product cEntity = await GetSingleByID(entity.ID);

                _context.Products.Add(entity);

                //su dung savechangeAsyn thi phai co await, tra ve se bat dong bbo vi cos nhieu asyn
                if (cEntity != null)
                {
                    cEntity.Name = entity.Name;
                    cEntity.UnitPrice = entity.UnitPrice;
                    cEntity.Image = entity.Image;
                    cEntity.ProductDate = entity.ProductDate;
                    cEntity.CategoryID = entity.CategoryID;
                    cEntity.Description = entity.Description;

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