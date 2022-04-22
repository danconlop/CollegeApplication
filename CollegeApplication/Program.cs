using System;
using System.Threading.Tasks;
using Model;

namespace CollegeApplication
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    await AppDbContextSeed.SeedAsync(context);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
            }

            var menu = new Menu();
            var showMenu = true;
            while (showMenu)
            {
                showMenu = menu.Show();
            }
        }
    }
}
