using Book.DataAccess.Data;
using Book.DataAccess.Repository.IRepository;
using Book.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Product obj)
        {
            var result = _db.Products.FirstOrDefault(x=>x.Id== obj.Id);
            if(result != null) 
            {
                result.Id = obj.Id;
                result.ISBN = obj.ISBN;
                result.Price = obj.Price;
                result.Description = obj.Description;
                result.CategoryId = obj.CategoryId;
                result.Author = obj.Author;
                result.ListPrice = obj.ListPrice;
                result.Price50 = obj.Price50;
                result.Price100 = obj.Price100;
                result.Title =  obj.Title;
                if(obj.ImageUrl!= null) 
                {
                    result.ImageUrl = obj.ImageUrl;
                }
            }
        }
    }
}
