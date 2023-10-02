using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using repository.Entities;
using repository.Imp.StoreContext;

namespace repository.Imp.Seed
{
    public static  class Seed
    {
        public static async Task AddRoleMookData(IServiceProvider serviceProvider)
        {
             using var context = new DataContext(serviceProvider.GetRequiredService<DbContextOptions<DataContext>>());

            if(await context.Role.AnyAsync()) return;

            var roles = new List<Role>(){
                new Role() {Name = "normal"},
                new Role() {Name = "admin"},
            };

            await context.Role.AddRangeAsync(roles);
            await context.SaveChangesAsync();

            Console.WriteLine(roles);

        }
    }
}