using Book.DataAccess.Data;
using Book.DataAccess.Repository.IRepository;
using Book.Models;
using Book.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Book.DataAccess.Repository
{
    public class ShoppingRepository : Repository<ShoppingCart>, IShoppingRepository
    {

        private ApplicationDbContext _db;
        public ShoppingRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }


        public void Update(ShoppingCart obj)
        {
            _db.shoppingCarts.Update(obj);
        }
    }
}
