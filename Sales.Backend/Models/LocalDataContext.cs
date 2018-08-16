

namespace Sales.Backend.Models
{
    using Domain.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    public class LocalDataContext : DataContext
    {
        public System.Data.Entity.DbSet<Sales.Common.Models.Product> Products { get; set; }
    }
}