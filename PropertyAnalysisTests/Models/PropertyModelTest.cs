using Microsoft.VisualStudio.TestTools.UnitTesting;
using PropertyAnalysisTool.Models;
using System;

namespace PropertyAnalysisTests.Models
{
    [TestClass]
    public class PropertyModelTest
    {
        [TestMethod]
        public void TestInitialRentEqual()
        {
            var prop = new PropertyModel();
            var initialYieldPercentage = prop.InitialYieldPercentage;
            var vacancyRate = 50;
            var price = 100000;

            prop.PriceDisplay = "100000";

            var initRent = Math.Round(initialYieldPercentage / 100 * price / vacancyRate);

            Assert.AreEqual(initRent, prop.InitialRent);
        }

        [TestMethod]
        public void TestInitialRentNotEqual()
        {
            var prop = new PropertyModel();
            var initialYieldPercentage = prop.InitialYieldPercentage;
            var vacancyRate = 50;
            var price = 1000000;

            prop.PriceDisplay = "100000";

            var initRent = Math.Round(initialYieldPercentage / 100 * price / vacancyRate);

            Assert.AreNotEqual(initRent, prop.InitialRent);
        }

        [TestMethod]
        public void TestAnnualLoanCostEqual()
        {
            var prop = new PropertyModel();
            var initialInterestRate = prop.InitialInterestRate;
            var price = 100000;
            prop.PriceDisplay = "100000";

            var initAnnInterest = price * initialInterestRate / 100;

            Assert.AreEqual(initAnnInterest, prop.AnnualLoanCost);
        }

        [TestMethod]
        public void TestAnnualLoanCostNotEqual()
        {
            var prop = new PropertyModel();
            var initialInterestRate = prop.InitialInterestRate;
            var price = 1000000;
            prop.PriceDisplay = "100000";

            var initAnnInterest = price * initialInterestRate / 100;

            Assert.AreNotEqual(initAnnInterest, prop.AnnualLoanCost);
        }
    }
}