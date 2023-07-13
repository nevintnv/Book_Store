﻿using Book.Models;
using Book.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.DataAccess.Repository.IRepository
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        void Update(OrderHeader obj);
        void UpdateStatus(int id,string orderStatus,string? paymentStatus = null);
        void UpdateStripePaymentId(int id, string SessionId, string PaymentIntendId);
    }
}
