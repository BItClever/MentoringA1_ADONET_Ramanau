namespace MentoringA1_ADONET_Ramanau
{
    public class CustOrderHist
    {
        public string ProductName { get; set; }
        public int? Total { get; set; }

        public CustOrderHist() 
        { 
            ProductName = null;
            Total = null; 
        }
    }
}
