using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using dxTestSolutionXPO.Module.BusinessObjects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dxTestSolutionXPO.Tests {
    [TestFixture]
    public class Complex: BaseTest {
        [Test]
        public void GetMax() {
            //arrange
            PopulateСollectionWithActive();
            var uow = new UnitOfWork();
            var join = new JoinOperand("Order", null, Aggregate.Max, new OperandProperty("Price"));
            var join2 = new BinaryOperator(nameof(Order.Price), join);
            //act
            var joinRes = new XPCollection<Order>(uow, join2).ToList();
            //assert
            Assert.AreEqual(50, joinRes[0].Price);

        }
    }
}
