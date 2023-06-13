﻿using Book.DataAccess.Data;
using Book.DataAccess.Repository.IRepository;
using Book.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.DataAccess.Repository
{
    public class UnitOfWork :IUnitOfWork
    {
        private ApplicationDbContext _db;

        public ICategoryRepository Category { get; private set; } 
        public IProductRepository Product { get; private set; }
        public ICompanyRepository Company { get; private set; }
        public IShoppingRepository Shopping { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public UnitOfWork (ApplicationDbContext db) 
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
            Company = new CompanyRepository(_db);
            Shopping = new ShoppingRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
        }
        //public ICategoryRepository CategoryRepository { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
