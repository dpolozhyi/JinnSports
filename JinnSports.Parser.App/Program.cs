using System;
using System.Collections.Generic;
using System.Linq;
using JinnSports.DAL.Repositories;
using JinnSports.DAL.Entities;
using JinnSports.Parser.App.JsonEntities;

namespace JinnSports.Parser.App
{
    class Program
    {
        static void Main(string[] args)
        {
            //JsonParser DEMO
            /*JsonParser jp = new JsonParser();
            JsonResult res = jp.DeserializeJson(jp.GetJsonFromUrl(jp.FonbetUri));
            foreach (var s in res.Sections)
            {
                Console.WriteLine("Id:{0}", s.Id);
                Console.WriteLine("Sport:{0}", res.Sports.Where(n => n.Id==s.Sport).Take(1).ToList()[0].Name);
                Console.WriteLine("Name:{0}", s.Name);
                Console.Write("Events:[ ");
                foreach(var e in s.Events)
                { 
                    Console.Write("{0} ",e.ToString());
                }
                Console.WriteLine("]\n");
            }*/

            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory());
            EFUnitOfWork unitOfWork = new EFUnitOfWork();
            Team t1 = new Team()
            {
                Name = "ManchesterCity",
                SportType = new SportType()
                {
                    Name = "Football"
                }
            };
            unitOfWork.Teams.Create(t1);
            Team t2 = new Team()
            {
                Name = "ManchesterUnited",
                SportType = new SportType()
                {
                    Name = "Tennis"
                }
            };
            //unitOfWork.Teams.Create(t2);
            unitOfWork.Save();
            //Console.ReadKey();
            Console.WriteLine("Объекты успешно сохранены");
            /* var teams = unitOfWork.Teams.GetAll();
             Console.WriteLine("Список объектов:");
             foreach (Team t in teams)
             {
                 Console.WriteLine("{0}.{1} - {2}", t.Id, t.Name, t.SportType);
             }*/
            //Console.ReadKey();
        }
    }
}
