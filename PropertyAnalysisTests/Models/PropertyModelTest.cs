using Microsoft.VisualStudio.TestTools.UnitTesting;
using PropertyAnalysisTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void TestAnnualInterestCostEqual()
        {
            var prop = new PropertyModel();
            var initialInterestRate = prop.InitialInterestRate;
            var price = 100000;
            prop.PriceDisplay = "100000";

            var initAnnInterest = price * initialInterestRate / 100;

            Assert.AreEqual(initAnnInterest, prop.AnnualInterestCost);
        }

        [TestMethod]
        public void TestAnnualInterestCostNotEqual()
        {
            var prop = new PropertyModel();
            var initialInterestRate = prop.InitialInterestRate;
            var price = 1000000;
            prop.PriceDisplay = "100000";

            var initAnnInterest = price * initialInterestRate / 100;

            Assert.AreNotEqual(initAnnInterest, prop.AnnualInterestCost);
        }


    }
}
