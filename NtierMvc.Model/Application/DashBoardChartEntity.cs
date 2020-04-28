using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NtierMvc.Model.Application
{
    public class ChartPoint
    {
        public string XValue { get; set; }
        public string YValue { get; set; }
    }

    public class ChartPointDetails
    {
        public List<ChartPoint> LstChartPoint { get; set; }

        public string ChartTitle { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public ChartPointDetails()
        {
            LstChartPoint = new List<ChartPoint>();
        }

    }


}
