using SamuraiApp.Data;
using SamuraiApp.Domain;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SomeUI
{
    internal static class Module4Methods
    {
        private static SamuraiContext _context = new SamuraiContext();
        public static void RunAll()
        {
            //InsertSamurai();
            //InsertMultipleSamurais();
            //SimpleSamuraiQuery();
            //MoreQueries();
            //RetrieveAndUpdateSamurai();
            //RetrieveAndUpdateMultipleSamurai();
            //QueryAndUpdateSamuraiDisconnected();
            //QueryAndUpdateDisconnectedBattle();
            //DeleteWhileTracked();
            //DeleteMany();
            //DeleteWhileNotTracked();
            //RawSqlQuery();
            //QueryWithNonSql();
            //RawSqlCommand();
            //RawSqlQuery();
        }
        private static void RawSqlCommand()
        {
            var affected = _context.Database.ExecuteSqlCommand(
                "update Samurais set Name = REPLACE(Name,'2234','1111')");
            Console.WriteLine($"Affected Rows ---- {affected}");
        }

        private static void QueryWithNonSql()
        {
            var samurais = _context.Samurais
                .Select(s => new { newName = ReverseString(s.Name) })
                .ToList();
            samurais.ForEach(s => Console.WriteLine(s.newName));
        }

        private static string ReverseString(string name)
        {
            var stringChar = name.AsEnumerable();
            return string.Concat(stringChar.Reverse());
        }

        private static void RawSqlQuery()
        {
            var samurais = _context.Samurais.FromSql("select * from Samurais")
                .OrderByDescending(s => s.Name).ToList();
            samurais.ForEach(s => Console.WriteLine("Name............." + s.Name));
            //var namePart = "San";
            //var samurais = _context.Samurais
            //    .FromSql("EXEC FilterSamuraiByNamePart {0}", namePart)
            //    .OrderByDescending(s => s.Name).ToList();
            //
            //samurais.ForEach(s=>Console.WriteLine("Name............. "+s.Name));
            //Console.WriteLine();
        }

        private static void DeleteWhileNotTracked()
        {
            var samurai = _context.Samurais.FirstOrDefault(s => s.Name == "Sampso0nSan");
            using (var contextNewAppInstance = new SamuraiContext())
            {
                contextNewAppInstance.Samurais.Remove(samurai);
                contextNewAppInstance.SaveChanges();
            }
        }

        private static void DeleteMany()
        {
            var samurais = _context.Samurais.Where(s => s.Name.Contains("ssss"));
            _context.Samurais.RemoveRange(samurais);
            _context.SaveChanges();
        }

        private static void DeleteWhileTracked()
        {
            var samurai = _context.Samurais.FirstOrDefault(s => s.Name == "KikuchiyoSanSan");
            _context.Samurais.Remove(samurai);
            _context.SaveChanges();
        }

        private static void QueryAndUpdateDisconnectedBattle()
        {
            var battle = _context.Battles.FirstOrDefault();
            battle.EndDate = new DateTime(1754, 12, 31);
            using (var contextNewAppInstance = new SamuraiContext())
            {
                contextNewAppInstance.Battles.Update(battle);
                contextNewAppInstance.SaveChanges();
            }
        }
        private static void RetrieveAndUpdateMultipleSamurai()
        {
            var samurai = _context.Samurais.ToList();
            samurai.ForEach(s => s.Name += "San");
            _context.SaveChanges();
        }

        private static void RetrieveAndUpdateSamurai()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Name += "San";
            _context.SaveChanges();
        }

        private static void QueryAndUpdateSamuraiDisconnected()
        {
            var samurai = _context.Samurais.FirstOrDefault(s => s.Name == "Kikuchiyo");
            samurai.Name += "San";
            using (var contextNewAppInstance = new SamuraiContext())
            {
                contextNewAppInstance.Samurais.Update(samurai);
                contextNewAppInstance.SaveChanges();
            }
        }

        private static void MoreQueries()
        {
            var name = "Sampson";
            var samurais = _context.Samurais.Where(s => s.Name == name).FirstOrDefault();
        }

        private static void SimpleSamuraiQuery()
        {
            using (var context = new SamuraiContext())
            {
                var samurais = context.Samurais.ToList();
                var query = context.Samurais;
                foreach (var samurai in context.Samurais)
                //foreach (var samurai in query)
                {
                    Console.WriteLine(samurai.Name);
                }
            }
        }

        private static void InsertMultipleSamurais()
        {
            var samurai = new Samurai { Name = "Julie" };
            var samuraiSammy = new Samurai { Name = "Sampson" };
            using (var context = new SamuraiContext())
            {
                context.GetService<ILoggerFactory>().AddProvider(new MyLoggerProvider());
                context.Samurais.AddRange(new List<Samurai> { samurai, samuraiSammy });
                context.SaveChanges();
            }
        }

        private static void InsertSamurai()
        {
            var samurai = new Samurai { Name = "Julie" };
            using (var context = new SamuraiContext())
            {
                context.GetService<ILoggerFactory>().AddProvider(new MyLoggerProvider());
                context.Samurais.Add(samurai);
                context.SaveChanges();
            }
        }

    }
}
