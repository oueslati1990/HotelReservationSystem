﻿using Microsoft.EntityFrameworkCore;
using Payment.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.API.Data
{
    public class PaymentContext : DbContext
    {
        public PaymentContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<PaymentData> PaymentDatas { get; set; }
    }
}
