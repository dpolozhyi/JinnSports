using System;
using System.Collections.Generic;
using JinnSports.DAL.Repositories;
using JinnSports.DAL.Entities;

namespace JinnSports.Parser.App
{
    class Program
    {
        static void Main(string[] args)
        {
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
