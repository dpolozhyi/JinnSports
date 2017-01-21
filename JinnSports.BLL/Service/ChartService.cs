using JinnSports.BLL.Dtos.Charts;
using JinnSports.BLL.Interfaces;
using JinnSports.DataAccessInterfaces.Interfaces;
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



            return dataTable;
        }
    }
}