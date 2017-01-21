using System.Collections.Generic;
using System.Linq;
using JinnSports.BLL.Dtos.Charts;
using JinnSports.BLL.Interfaces;
using JinnSports.DataAccessInterfaces.Interfaces;
using JinnSports.Entities.Entities;
using log4net;

namespace JinnSports.BLL.Service
{
    public class ChartService : IChartService
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ChartService));

        private readonly IUnitOfWork unitOfWork;

        public ChartService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public GoogleVisualizationDataTable GetDataTableForTeam(int id)
        {
            GoogleVisualizationDataTable dataTable = new GoogleVisualizationDataTable();

            dataTable.AddColumn("Date", "string");
            dataTable.AddColumn("WinRate", "number");

            var results = unitOfWork.GetRepository<Team>().GetById(id).Results.OrderBy(result => result.SportEvent.Date);
            int winCounter = 0;

            foreach (var result in results)
            {
                IList<object> row = new List<object>(2);
                row.Add(result.SportEvent.Date.ToShortDateString());

                var winners = result.SportEvent.Results.Where(r => r.Score == result.SportEvent.Results.Max(mr => mr.Score));

                dataTable.AddRow(row);
            }

            return dataTable;
        }
    }
}