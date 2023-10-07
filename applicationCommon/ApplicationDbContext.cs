using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using xingyi.application;
using xingyi.job.Models;

namespace xingyi.application
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }



    }
}
