﻿using GroupBy.Data.DbContexts;
using GroupBy.Design.DbContext;
using GroupBy.Design.UnitOfWork;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupBy.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork<GroupByDbContext>
    {
        private static readonly object lockObject = new object();

        public Stack<UnitOfWork> UnitOfWorks
        {
            get
            {
                return unitOfWorkLocator.UnitOfWorks;
            }
        }

        private readonly IDbContextLocator<GroupByDbContext> dbContextLocator;
        private readonly IDbContextFactory<GroupByDbContext> dbContextFactory;
        private readonly IUnitOfWorkLocator<UnitOfWork> unitOfWorkLocator;

        public bool IsDisposed { get; private set; } = false;

        public UnitOfWork Parent { get; private set; }

        public bool IsRoot
        {
            get => Parent == null;
        }

        private GroupByDbContext dbContext
        {
            get => dbContextLocator.GetDbContext();
        }

        public GroupByDbContext DbContext { get => dbContext; }

        private IDbContextTransaction transaction
        {
            get => dbContext.Database.CurrentTransaction;
        }

        public UnitOfWork(
            IDbContextLocator<GroupByDbContext> dbContextLocator,
            IDbContextFactory<GroupByDbContext> dbContextFactory,
            IUnitOfWorkLocator<UnitOfWork> unitOfWorkLocator)
        {
            this.dbContextLocator = dbContextLocator;
            this.dbContextFactory = dbContextFactory;
            this.unitOfWorkLocator = unitOfWorkLocator;

            lock (lockObject)
            {
                if (dbContext == null)
                {
                    var context = dbContextFactory.CreateDbContext();
                    dbContextLocator.SetDbContext(context);
                }

                if (UnitOfWorks.Any())
                {
                    Parent = UnitOfWorks.Peek();
                }

                UnitOfWorks.Push(this);
            }
        }

        public Task Commit()
        {
            if (IsRoot)
                return dbContext.SaveChangesAsync();

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            lock (lockObject)
            {
                if (IsDisposed)
                {
                    return;
                }

                if (IsRoot)
                {
                    dbContextLocator.RemoveDbContext();
                }

                UnitOfWorks.Pop();
                IsDisposed = true;
            }
        }

        public IDbContextTransaction BeginTransaction()
        {
            if (IsRoot)
                return dbContext.Database.BeginTransaction();

            return null;
        }

        public void RollbackTransaction()
        {
            if (IsRoot && transaction != null)
                transaction.Rollback();
        }

        public void CommitTransaction()
        {
            if (IsRoot && transaction != null)
                transaction.Rollback();
        }
    }
}
