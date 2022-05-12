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
    public class Aggregate_Single : BaseTest {
        [Test]
        public void Task0_0() {
            //arrange
            PopulateSelectFromCollection();
            var uow = new UnitOfWork();
            //act
            CriteriaOperator criterion = CriteriaOperator.Parse("[OrderItems][ItemPrice=40].Single([OrderItemName])");
            CriteriaOperator filterParentCollection= new BinaryOperator(nameof(Order.OrderName), "FirstName2");
            var result3 = uow.Evaluate<Order>(criterion, filterParentCollection);
            //assert
            Assert.AreEqual("Task2-1", result3);
        }
        [Test]
        public void Task0_1() {
            //arrange
            PopulateSelectFromCollection();
            var uow = new UnitOfWork();
            //act
            CriteriaOperator criterion = new AggregateOperand(new OperandProperty(nameof(Order.OrderItems)), new OperandProperty(nameof(OrderItem.OrderItemName)), Aggregate.Single, new BinaryOperator(nameof(OrderItem.ItemPrice), 40));
            CriteriaOperator filterParentCollection = new BinaryOperator(nameof(Order.OrderName), "FirstName2");
            var result3 = uow.Evaluate<Order>(criterion, filterParentCollection);
            //assert
            Assert.AreEqual("Task2-1", result3);
        }
        [Test]
        public void Task0_2() {
            //arrange
            PopulateSelectFromCollection();
            var uow = new UnitOfWork();
            //act
            CriteriaOperator criterion = CriteriaOperator.FromLambda<Order, string>(o => o.OrderItems.SingleOrDefault(oi => oi.ItemPrice == 40).OrderItemName);
            CriteriaOperator filterParentCollection = new BinaryOperator(nameof(Order.OrderName), "FirstName2");
            var result3 = uow.Evaluate<Order>(criterion, filterParentCollection);
            //assert
            Assert.AreEqual("Task2-1", result3);
        }
        [Test]
        public void Task2_0() {
            //arrange
            PopulateSelectFromCollection();
            var uow = new UnitOfWork();
            //act
            CriteriaOperator criterion = CriteriaOperator.Parse("[OrderItems][ItemPrice=40].Single()");
            CriteriaOperator filterParentCollection = new BinaryOperator(nameof(Order.OrderName), "FirstName2");
            var result3 = uow.Evaluate<Order>(criterion, filterParentCollection);
            //assert
            throw new Exception();
            //TODO!!!!!!!describe between diff values on server (object) and client (id)
            var reqId = uow.FindObject<OrderItem>(new BinaryOperator(nameof(OrderItem.OrderItemName), "Task2-1")).Oid;
            Assert.AreEqual(reqId, result3);
        }
        [Test]
        public void Task2_1() {
            //arrange
            PopulateSelectFromCollection();
            var uow = new UnitOfWork();
            //act
            CriteriaOperator criterion = new AggregateOperand(new OperandProperty(nameof(Order.OrderItems)), new OperandProperty("This"), Aggregate.Single, new BinaryOperator(nameof(OrderItem.ItemPrice), 40));
            CriteriaOperator filterParentCollection = new BinaryOperator(nameof(Order.OrderName), "FirstName2");
            var result = uow.Evaluate<Order>(criterion, filterParentCollection);
            //assert

            //describe between diff values on server (object) and client (id)
            var reqId = uow.FindObject<OrderItem>(new BinaryOperator(nameof(OrderItem.OrderItemName), "Task2-1")).Oid;
            Assert.AreEqual(reqId, result);
        }
        [Test]
        public void Task2_2() {
            //arrange
            PopulateSelectFromCollection();
            var uow = new UnitOfWork();
            //act
            CriteriaOperator criterion = CriteriaOperator.FromLambda<Order, OrderItem>(o => o.OrderItems.SingleOrDefault(oi => oi.ItemPrice == 40));
            CriteriaOperator filterParentCollection = new BinaryOperator(nameof(Order.OrderName), "FirstName2");
            var result = uow.Evaluate<Order>(criterion, filterParentCollection);
            //assert

            //describe between diff values on server (object) and client (id)
            var reqId = uow.FindObject<OrderItem>(new BinaryOperator(nameof(OrderItem.OrderItemName), "Task2-1")).Oid;
            Assert.AreEqual(reqId, result);
        }













        [Test]
        public void Task0() {
            //arrange
            PopulatePlainCollection();
            //act
            var uow = new UnitOfWork();
            // var coll =
            //assert
            Assert.Throws<InvalidOperationException>(() => { new XPCollection<Order>(uow).Single(); });
        }
        [Test]
        [Ignore]
        public void Task1_0_x() {
            //arrange
            PopulateSelectFromCollection();
            var uow=new UnitOfWork();
            var join = new JoinOperand("Order", null, Aggregate.Max, new OperandProperty("Price"));
            var join2 = new BinaryOperator(nameof(Order.Price), join);
            var joinRes = new XPCollection<Order>(uow, join2).ToList();

            var maxCrit = new AggregateOperand(null, nameof(Order.Price), Aggregate.Max);
            var maxCrit2 = new BinaryOperator(nameof(Order.Price), maxCrit);
            var resMasx = uow.Evaluate<Order>(maxCrit2, null);
            var resMasx2 = new XPCollection<Order>(uow, maxCrit2).ToList(); 


            var crit3 = new AggregateOperand(nameof(Order.OrderItems), nameof(OrderItem.ItemPrice), Aggregate.Single);
            var crit4 = new AggregateOperand(null, nameof(Order.Price), Aggregate.Max);
            var crit5 = new BinaryOperator(nameof(Order.Price), crit4);
            var crit6 = new BinaryOperator(nameof(Order.Price), crit3);
            var crit = new AggregateOperand(nameof(Order.OrderItems), null, Aggregate.Single);
            var crit2 = new BinaryOperator(crit, 100, BinaryOperatorType.Greater);
            var res = new XPCollection<Order>(uow, crit5).ToList();
            var res2 = new XPCollection<Order>(uow, crit6).ToList();


            CriteriaOperator criterion = CriteriaOperator.Parse("[OrderItems][ItemPrice=40].Single([OrderItemName])");
            CriteriaOperator criterion2 = CriteriaOperator.Parse("Single([Price])");
            //act
            var result = uow.Evaluate<Order>(criterion,null );
            var result2 = uow.Evaluate<Order>(crit3, null);
            var result3 = uow.Evaluate<Order>(criterion, new BinaryOperator(nameof(Order.OrderName), "FirstName2"));
            
            //assert
            Assert.AreEqual("FirstName1", result);


        }


    }
}
