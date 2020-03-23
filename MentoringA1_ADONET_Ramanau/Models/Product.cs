namespace MentoringA1_ADONET_Ramanau.Models
{
    public class Product
    {
        public int? ProductID { get; set; }
        public string ProductName { get; set; }
        public int? SupplierID { get; set; }
        public int? CategoryID { get; set; }
        public string QuantityPerUnit { get; set; }
        public float? UnitPrice { get; set; }
        public int? UnitslnStock { get; set; }
        public int? UnitsOnOrder { get; set; }
        public int? ReorderLevel { get; set; }
        public int? Discountinued { get; set; }

        public Product() 
        { 
            ProductID = null;
            ProductName = null; 
            SupplierID = null; 
            CategoryID = null;
            QuantityPerUnit = null;
            UnitPrice = null;
            UnitslnStock = null;
            UnitsOnOrder = null;
            ReorderLevel = null;
            Discountinued = null;
        }
    }
}
