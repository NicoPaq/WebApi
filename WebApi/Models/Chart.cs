using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Charts
    {
        public List<Chart1> lstCharts1 { get; set; }
        public List<Chart2> lstCharts2 { get; set; }
        public List<Chart3> lstCharts3 { get; set; }
    }

   
    public class Chart1
    {
        public int NpsScore { get; set; }
        public int Day { get; set; }
        public string DayLib { get; set; }
        public string Css { get; set; }
    }

    public class Chart2
    {
        public int Day { get; set; }
        public string DayLib { get; set; }
        public int NbDetractor { get; set; }
        public int NbPassive { get; set; }
        public int NbActive { get; set; }
        public decimal PourcentageDetractor { get; set; }
        public decimal PourcentagePassive { get; set; }
        public decimal PourcentageActive { get; set; }
    }

    public class Chart3
    {
        public int Score { get; set; }
        public int ScoreValue { get; set; }
        public string Type { get; set; }
    }
}