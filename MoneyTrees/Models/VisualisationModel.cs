using System.Collections.Generic;

namespace MoneyTrees.Models
{
    public class VisualisationModel
    {
        public List<List<double>> StockChartData { get; set; }

        public VisualisationCalculation VisualisationCalculation { get; set; }

        public List<CategorySum> CategorySums {get; set;}
    }

    public class VisualisationCalculation
    {
        public double AverageTranscationCost { get; set; }

        public double AmountSpent { get; set; }

        public double AmmountSpentThisMonth { get; set; }

    }
}