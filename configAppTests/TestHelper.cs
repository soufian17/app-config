using config_app.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace configAppTests
{
    public static class TestHelper
    {
        public static void InjectData<T>(DbContextOptions<ConfigAppContext> options, params T[] entities) where T : class
        {
            using ConfigAppContext context = new ConfigAppContext(options);
            context.Set<T>().AddRange(entities);
            context.SaveChanges();
        }
    }
}
