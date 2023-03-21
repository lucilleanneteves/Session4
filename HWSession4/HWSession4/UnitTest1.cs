using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Diagnostics.Metrics;
using System.Drawing;

namespace HW4ServiceReference
{
    [TestClass]
    public class CountryList
    {
        //Global Variable
        private readonly HW4ServiceReference.CountryInfoServiceSoapTypeClient countryListTest =
             new HW4ServiceReference.CountryInfoServiceSoapTypeClient(HW4ServiceReference.CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);

        [TestMethod]
        public void AscendingCountryCode()
        {
            // Verify Ascending Order of Country Code
            var countryCode = countryListTest.ListOfCountryNamesByCode();
            var countryCodeAsc = countryCode.OrderBy(isoCode => isoCode.sISOCode);
            Assert.IsTrue(Enumerable.SequenceEqual(countryCodeAsc, countryCode), "Not Ascending Order");
        }

        [TestMethod]
        public void InvalidCountryCode()
        {
            // Verify Passing of Invalid Country Code
            var countryName = countryListTest.CountryName("AD");

            // To verify that method get a country when country code is correct
            Assert.AreEqual("Andorra", countryName, "Country not found in the database");

            // To verify that invalid country code is not found in the database
            Assert.AreEqual("Country not found in the database", countryListTest.CountryName("234"), "Country is found in the database");
        }

        [TestMethod]
        public void SameCountryName()
        {
            // Validate the Country Name from both API is the same
            var lastCountryCode = countryListTest.ListOfCountryNamesByCode().Last();
            var countryName = countryListTest.CountryName(lastCountryCode.sISOCode);
            Assert.AreEqual(lastCountryCode.sName, countryName, "Not Equal");
        }
    }
}