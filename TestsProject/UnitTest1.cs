using MentoringA1_ADONET_Ramanau;
using MentoringA1_ADONET_Ramanau.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace TestsProject
{
    [TestFixture]
    public class UnitOfWorkTests
    {
        private UnitOfWork unitOfWork;

        [SetUp]
        public void SetUp()
        {
            unitOfWork = new UnitOfWork();
        }

        [Test]
        public void TestCustOrderHist()
        {
            List<CustOrderHist> test = unitOfWork.CustOrderHist("ANATR");
            Assert.IsTrue(test.Count > 0);

        }

        [Test]
        public void TestAddOrder()
        {
            bool test = unitOfWork.AddOrder(new Order());

            Assert.IsTrue(test);
        }

        [Test]
        public void TestChangeOrderDate()
        {
            bool test = unitOfWork.ChangeOrderDate(DateTime.Now, 10248);

            Assert.IsTrue(test);
        }

        [Test]
        public void TestChangeShippedDate()
        {          
            bool test = unitOfWork.ChangeShippedDate(DateTime.Now, 10248);

            Assert.IsTrue(test);
        }

        [Test]
        public void TestCustOrdersDetail()
        {
            List<CustOrdersDetails> test = unitOfWork.CustOrdersDetail("10248");

            Assert.IsTrue(test.Count > 0);
        }

        [Test]
        public void TestGetOrderById()
        {
            Order test = unitOfWork.GetOrderById(10248);

            Assert.IsTrue(test.ShipName != null);
        }

        [Test]
        public void TestShowOrders()
        {
            List<Order> test = unitOfWork.GetAllOrders();

            Assert.IsTrue(test.Count > 0);
        }
    }
}
