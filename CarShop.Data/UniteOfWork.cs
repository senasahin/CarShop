﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Data
{
    // birden fazla repository işlemini tek bir transaction içinde çalıştırmayı sağlar
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext db;
        public UnitOfWork(ApplicationDbContext db)
        {
            this.db = db;
        }

        private DbContextTransaction transaction;
        public void BeginTransaction()
        {
            transaction = db.Database.BeginTransaction();
        }
        public void Commit()
        {
            transaction.Commit();
        }
        public void Rollback()
        {
            transaction.Rollback();
        }
        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}
