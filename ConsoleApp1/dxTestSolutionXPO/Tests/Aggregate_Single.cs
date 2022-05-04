﻿using DevExpress.Data.Filtering;
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
        public void Task1_0() {
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


            CriteriaOperator criterion = CriteriaOperator.Parse("[OrderItems].Single([OrderItemName])");
            //  CriteriaOperator criterion = CriteriaOperator.Parse("Single([Price])");
            //act
            var result = uow.Evaluate<Order>(criterion,null );
            var result2 = uow.Evaluate<Order>(crit3, null);
            
            //assert
            Assert.AreEqual("FirstName1", result);


        }
        [Test]
        public void Task1_1() {
            //arrange
            PopulatePlainCollection();
            var uow = new UnitOfWork();

            //act
            var result = new XPCollection<Order>(uow).Single(x => x.Price == 20);
            //assert
            Assert.AreEqual("FirstName1", result.OrderName);


        }


    }
}
