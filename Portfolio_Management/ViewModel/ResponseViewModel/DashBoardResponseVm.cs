namespace Portfolio_Management.ViewModel.ResponseViewModel
{
    public class DashBoardResponseVm
    {
        public double TotalUnit { get; set; }
        public double TotalInvestment { get; set; }
        public double TotalSold { get; set; }
        public decimal? CurrentAmount { get; set; }
        public double OverAllProfit => (TotalInvestment - TotalSold);
    }
}