using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using JinnSports.BLL.Interfaces;
using JinnSports.BLL.Service;
using JinnSports.DAL.EFContext;
using JinnSports.DAL.Repositories;
using NUnit.Framework;

namespace JinnSports.UnitTests.Services
{
    [TestFixture]
    public class ChartServiceTests
    {
        private IChartService chartService;
        
        private SportsContext databaseSportsContext;

        private TransactionScope databaseTransaction;

        [OneTimeSetUp]
        public void OneTimeInit()
        {
            this.databaseSportsContext = new SportsContext("SportsContext");

            // Clear tables
            this.databaseSportsContext.TeamNames.RemoveRange(
                this.databaseSportsContext.TeamNames);
            this.databaseSportsContext.Results.RemoveRange(
                this.databaseSportsContext.Results);
            this.databaseSportsContext.SportEvents.RemoveRange(
                this.databaseSportsContext.SportEvents);
            this.databaseSportsContext.Teams.RemoveRange(
                this.databaseSportsContext.Teams);
            this.databaseSportsContext.SportTypes.RemoveRange(
                this.databaseSportsContext.SportTypes);

            this.databaseSportsContext.SaveChanges();

            this.chartService = new ChartService(new EFUnitOfWork(databaseSportsContext));

            this.databaseSportsContext.Database.ExecuteSqlCommand(
                @"SET IDENTITY_INSERT [dbo].[SportTypes] ON;
                INSERT INTO [dbo].[SportTypes] ([Id], [Name])
                VALUES
                (1, 'Football');               
                SET IDENTITY_INSERT [dbo].[SportTypes] OFF;");

            this.databaseSportsContext.Database.ExecuteSqlCommand(
                @"SET IDENTITY_INSERT [dbo].[Teams] ON;
                INSERT INTO [dbo].[Teams] ([Id], [Name], [SportType_Id])
                VALUES
                (1, 'Manchester United', 1),
                (2, 'Milano', 1);               
                SET IDENTITY_INSERT [dbo].[Teams] OFF;");

            this.databaseSportsContext.Database.ExecuteSqlCommand(
                @"SET IDENTITY_INSERT [dbo].[TeamNames] ON;
                INSERT INTO [dbo].[TeamNames] ([Id], [Name], [Team_Id])
                VALUES
                (1, 'Manchester United', 1),
                (2, 'Milano', 2);               
                SET IDENTITY_INSERT [dbo].[TeamNames] OFF;");

            this.databaseSportsContext.Database.ExecuteSqlCommand(
                @"SET IDENTITY_INSERT [dbo].[SportEvents] ON;
                INSERT INTO [dbo].[SportEvents] ([Id], [Date], [SportType_Id])
                VALUES
                (1, '20161119 17:00', 1),
                (2, '20161028 17:00', 1),
                (3, '20161017 18:00', 1),
                (4, '20161103 16:00', 1),
                (5, '20161105 16:00', 2),
                (6, '20161129 16:00', 2),
                (7, '20161115 16:00', 2);               
                SET IDENTITY_INSERT [dbo].[SportEvents] OFF;");

            this.databaseSportsContext.Database.ExecuteSqlCommand(
                @"SET IDENTITY_INSERT [dbo].[Results] ON;
                INSERT INTO [dbo].[Results] ([Id], [Score], [SportEvent_Id], [Team_Id])
                VALUES
                (1, 2, 1, 1),
                (2, 1, 2, 2),
                (3, 3, 4, 2),
                (4, 1, 1, 3),
                (5, 0, 3, 3),
                (6, 0, 3, 4),
                (7, 2, 4, 4),
                (8, 4, 2, 5),
                (9, 68, 5, 6),
                (10, 52, 6, 6),
                (11, 65, 5, 7),
                (12, 65, 7, 7),
                (13, 64, 7, 8),
                (14, 52, 6, 8);               
                SET IDENTITY_INSERT [dbo].[Results] OFF;");

            this.databaseSportsContext.SaveChanges();
        }
    }
}
