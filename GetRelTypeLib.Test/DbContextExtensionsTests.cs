using GetRelTypeLib.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GetRelTypeLib.Test
{
    [TestClass]
    public class DbContextExtensionsTests
    {
        [TestMethod]
        public void Order_OrderDetail_Should_Be_OneToMany()
        {
            RelationshipType relationType;
            using (var context = new NorthwindSlim())
            {
                relationType = context.GetRelationshipType(typeof(Order), nameof(Order.OrderDetails));
            }
            Assert.AreEqual(RelationshipType.OneToMany, relationType);
        }

        [TestMethod]
        public void OrderDetail_Product_Should_Be_ManyToOne()
        {
            RelationshipType relationType;
            using (var context = new NorthwindSlim())
            {
                relationType = context.GetRelationshipType(typeof(OrderDetail), nameof(OrderDetail.Product));
            }
            Assert.AreEqual(RelationshipType.ManyToOne, relationType);
        }
    }
}
