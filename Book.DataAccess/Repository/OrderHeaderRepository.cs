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
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {

        private ApplicationDbContext _db;
        public OrderHeaderRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }


        public void Update(OrderHeader obj)
        {
            _db.orderHeaders.Update(obj);
        }

        public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
        {
            var orderfromDb = _db.orderHeaders.FirstOrDefault(x=>x.Id == id);
            if (orderfromDb != null) 
            {
                orderfromDb.OrderStatus = orderStatus;
                if(!string.IsNullOrEmpty(paymentStatus))
                {
                    orderfromDb.PaymentStatus = paymentStatus;
                }
            }
        }

        public void UpdateStripePaymentId(int id, string PaymentIntendId, string SessionId)
        {
            var orderfromDb = _db.orderHeaders.FirstOrDefault(x => x.Id == id);
            if (!string.IsNullOrEmpty(SessionId))
            {
                orderfromDb.SessionId = SessionId;
            }
            if (!string.IsNullOrEmpty(PaymentIntendId))
            {
                orderfromDb.PaymentIntendId = PaymentIntendId;
                orderfromDb.PaymentDate = DateTime.Now;
            }
        }
    }
}
