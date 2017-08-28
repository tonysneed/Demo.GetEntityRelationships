using GetRelTypeLib.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace GetRelTypeLib.Test
{
    [TestClass]
    public class EntityExtensionsTests
    {
        [TestMethod]
        public void GetNavigationProperties_Should_Return_Customer_Properties()
        {
            IEnumerable<NavigationProperty> navProps;

            using (var context = new NorthwindSlim())
            {
               navProps = context.GetNavigationProperties(typeof(Customer));
            }
            var custSettingProp = navProps.FirstOrDefault(p => p.Name == "CustomerSetting");
            var ordersProp = navProps.FirstOrDefault(p => p.Name == "Orders");
            Assert.IsNotNull(custSettingProp);
            Assert.IsNotNull(ordersProp);
        }
    }
}
