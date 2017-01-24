using System.Collections.Generic;
using System.Linq;

namespace JinnSports.WEB.Models
{
    public class GoogleVisualizationDataTable
    {
        public IList<Col> Cols { get; } = new List<Col>();
        public IList<Row> Rows { get; } = new List<Row>();

        public void AddColumn(string label, string type)
        {
            this.Cols.Add(new Col { Label = label, Type = type });
        }

        public void AddRow(IList<object> values)
        {
            this.Rows.Add(new Row { C = values.Select(x => new Row.RowValue { V = x }) });
        }

        public class Col
        {
            public string Label { get; set; }
            public string Type { get; set; }
        }

        public class Row
        {
            public IEnumerable<RowValue> C { get; set; }
            public class RowValue
            {
                public object V { get; set; }
            }
        }
    }
}