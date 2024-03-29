﻿using Microsoft.EntityFrameworkCore;
using Order.Domain.Common;
using Order.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Infrastructure.Persistence
{
    public class OrderContext : DbContext
    {

        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {

        }

        public DbSet<OrderEntity> Orders { get; set; }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = "ms";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = "ms";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
