using System;

namespace MentoringA1_ADONET_Ramanau.Models
{

    public enum Status 
    { 
        New,
        InWork,
        Execut 
    }
    public class Order
    {
        private DateTime? shippedDate;
        public Status OrderStatus { get; private set; }
        public int? OrderID { get; set; }
        public string CustomerID { get; set; }
        public string EmployeeID { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate
        {
            get
            {
                return shippedDate;
            }

            set
            {
                if (value == null)
                {
                    OrderStatus = Status.New;
                }
                else if (value == null)
                {
                    OrderStatus = Status.InWork;
                }
                else if (value != null)
                {
                    OrderStatus = Status.Execut;
                }
            }
        }
        public string ShipVia { get; set; }
        public decimal? Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }
        public Product OrderedProduct { get; set; }

        public Order() 
        { 
            OrderedProduct = new Product();
        }
    }
}
