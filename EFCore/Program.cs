using System;
using System.Linq;
using System.Diagnostics;
using EFCoreContext;
using Model;

namespace EFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            CoreContext context = new CoreContext();
            {
                Stopwatch sw = Stopwatch.StartNew();
                for (int i = 0; i < 10_000_000; i++)
                {
                    var film = new Film();
                    film.Name = Guid.NewGuid().ToString();
                    context.Films.Add(film);
                    if (i % 1000 == 0)
                    {
                        context.SaveChanges();                        
                        Console.WriteLine(sw.ElapsedMilliseconds);
                        sw.Restart();

                        context.Dispose();
                        context = new CoreContext();
                    }
                }
                context.SaveChanges();
            }
            context.Dispose();

            //using (CoreContext context = new CoreContext())
            //{
            //    var films = context.Films.Take(10).ToList();
            //}
        }
    }
}
