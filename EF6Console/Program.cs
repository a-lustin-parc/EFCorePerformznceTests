using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using EFContext;
using System.Diagnostics;
using Model;
using EFStandartContext;

namespace EF6Console
{
    class Program
    {
        static void Main(string[] args)
        {
            int cnt = 100_000;

            Stopwatch total = Stopwatch.StartNew();
            SaveAllEF6(cnt);
            Console.WriteLine($"EF6 save: {total.Elapsed}");
            total.Restart();
            SaveAllEFCore(cnt);
            Console.WriteLine($"EFCore save: {total.Elapsed}");
            total.Restart();
            GetEF6(cnt);
            Console.WriteLine($"EF6 get: {total.Elapsed}");
            total.Restart();
            GetEFCore(cnt);
            Console.WriteLine($"EFCore get: {total.Elapsed}");
        }

        private static void SaveAllEFCore(int cnt)
        {
            StandartContext context = new StandartContext();
            {
                Stopwatch sw = Stopwatch.StartNew();
                for (int i = 0; i < cnt; i++)
                {
                    var film = new Film();
                    film.Name = Guid.NewGuid().ToString();
                    context.Films.Add(film);
                    if (i % 1000 == 0)
                    {
                        context.SaveChanges();
                        //Console.WriteLine(sw.ElapsedMilliseconds);
                        sw.Restart();

                        context.Dispose();
                        context = new StandartContext();
                    }
                }
                context.SaveChanges();
            }
            context.Dispose();
        }

        private static void SaveAllEF6(int cnt)
        {
            Context context = new Context();
            {
                Stopwatch sw = Stopwatch.StartNew();
                for (int i = 0; i < cnt; i++)
                {
                    var film = new Film();
                    film.Name = Guid.NewGuid().ToString();
                    context.Films.Add(film);
                    if (i % 1000 == 0)
                    {
                        context.SaveChanges();
                        //Console.WriteLine(sw.ElapsedMilliseconds);
                        sw.Restart();

                        context.Dispose();
                        context = new Context();
                    }
                }
                context.SaveChanges();
            }
            context.Dispose();
        }

        private static void GetEF6(int cnt)
        {
            using (Context context = new Context())
            {
                for (int i = 0; i < cnt; i++)
                {
                    var films = context.Films.Where(f => f.Name.StartsWith("a")).Take(50).ToList();
                }
            }
        }

        private static void GetEFCore(int cnt)
        {
            using (StandartContext context = new StandartContext())
            {
                for (int i = 0; i < cnt; i++)
                {


                    var films = context.Films.Where(f => f.Name.StartsWith("a")).Take(50).ToList();
                }
            }
        }
    }
}
