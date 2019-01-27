using System;
using System.Collections.Generic;
using WebApi.Models;
using System.Linq;
using System.Web;

namespace WebApi.Helpers
{
    public class ChartsHelper
    {
        public static List<Survey> GetRandomSurvey()
        {
            Random rnd = new Random();
            var lst = new List<Survey>();
            for (int i = 0; i < 15000; i++)
            {
                Survey survey = new Survey()
                {
                    Date = new DateTime(2018, rnd.Next(1, 12), rnd.Next(1, 29)),
                    Score = rnd.Next(0, 10)
                };
                lst.Add(survey);
            }
            return lst;
        }

        public static List<Chart1> SetDataForChart1(List<Survey> lst)
        {
            var lstResult = new List<Chart1>();
            for (var i = 1; i < 13; i++)
            {
                var totalByDay = lst.FindAll(x => x.Date.Month == i && x.Score >= 0 && x.Score <= 10).Count;
                var totalDetractors = lst.FindAll(x => x.Date.Month == i && x.Score >= 0 && x.Score < 7).Count;
                var totalPromoters = lst.FindAll(x => x.Date.Month == i && x.Score >= 9 && x.Score <= 10).Count;

                if (totalByDay == 0)
                    totalByDay = 1;

                var scoreNps = CalculateNps(totalByDay, totalPromoters, totalDetractors);
                var o = new Chart1
                {
                    Day = i,
                    DayLib = GetMonthLib(i),
                    NpsScore = Convert.ToInt32(scoreNps)
                };

                if (o.NpsScore < 0)
                {
                    o.Css = "point { size: 5; fill-color: #A60D12}";
                }

                if (o.NpsScore >= 0 && o.NpsScore < 50)
                {
                    o.Css = "point { size: 5; fill-color: #F8CD08}";
                }

                if (o.NpsScore >= 50)
                {
                    o.Css = "point { size: 5; fill-color: #5EAF00}";
                }
                lstResult.Add(o);
            }
            return lstResult;
        }

        public static List<Chart2> SetDataForChart2(List<Survey> lst)
        {
            var lstResult = new List<Chart2>();

            for (var i = 1; i < 13; i++)
            {
                var lstDetractorByYear = lst.FindAll(x => x.Date.Month == (i) && x.Score <= 6 && x.Score >= 0);
                var lstPassiveByYear = lst.FindAll(x => x.Date.Month == (i) && x.Score > 6 && x.Score < 9);
                var lstActiveByYear = lst.FindAll(x => x.Date.Month == (i) && x.Score >= 9 && x.Score <= 10);

                var o = new Chart2
                {
                    Day = i,
                    DayLib = GetMonthLib(i),
                    NbActive = lstActiveByYear.FindAll(x => x.Date.Month == (i)).Count,
                    NbDetractor = lstDetractorByYear.FindAll(x => x.Date.Month == (i)).Count,
                    NbPassive = lstPassiveByYear.FindAll(x => x.Date.Month == (i)).Count,
                };

                var nbTotal = o.NbPassive + o.NbActive + o.NbDetractor;
                if (nbTotal == 0)
                {
                    o.PourcentageActive = 0;
                    o.PourcentageDetractor = 0;
                    o.PourcentagePassive = 0;
                }
                else
                {
                    o.PourcentageActive = decimal.Round((Convert.ToDecimal(o.NbActive) / Convert.ToDecimal(nbTotal)) * 100, 2, MidpointRounding.ToEven);
                    o.PourcentageDetractor = decimal.Round((Convert.ToDecimal(o.NbDetractor) / Convert.ToDecimal(nbTotal)) * 100, 2, MidpointRounding.ToEven);
                    o.PourcentagePassive = decimal.Round((Convert.ToDecimal(o.NbPassive) / Convert.ToDecimal(nbTotal)) * 100, 2, MidpointRounding.ToEven);
                }
                lstResult.Add(o);
            }
            return lstResult;
        }

        public static List<Chart3> SetDataForChart3(List<Survey> lst)
        {
            var lstResult = new List<Chart3>();
            for (var i = 0; i < 11; i++)
            {
                var o = new Chart3
                {
                    Score = i,
                    ScoreValue = lst.FindAll(x => x.Score == i).Count
                };

                lstResult.Add(o);
            }
            return lstResult;
        }

        internal static decimal CalculateNps(int total, int promoters, int detractors)
        {
            return Convert.ToInt32(decimal.Round(Convert.ToDecimal(promoters) / Convert.ToDecimal(total) * 100, 2,
                                       MidpointRounding.ToEven) -
                                   decimal.Round(Convert.ToDecimal(detractors) / Convert.ToDecimal(total) * 100, 2,
                                       MidpointRounding.ToEven));
        }

        internal static string GetMonthLib(int month)
        {
            switch (month)
            {
                case 1:
                    return "Jan.";
                case 2:
                    return "Feb.";
                case 3:
                    return "Mar.";
                case 4:
                    return "Apr.";
                case 5:
                    return "May";
                case 6:
                    return "Jun.";
                case 7:
                    return "Jul.";
                case 8:
                    return "Aug.";
                case 9:
                    return "Sep.";
                case 10:
                    return "Oct.";
                case 11:
                    return "Nov.";
                case 12:
                    return "Dec.";
                default:
                    return "";
            }
        }
    }
}