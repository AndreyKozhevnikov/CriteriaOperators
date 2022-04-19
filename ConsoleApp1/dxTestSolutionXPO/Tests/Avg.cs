using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using dxTestSolutionXPO.Module.BusinessObjects;
using NUnit.Framework;
using System;
using System.Linq;

namespace dxTestSolutionXPO {
    [TestFixture]
    public class CriteriaOperatorAvg {
        public CriteriaOperatorAvg() {
            // ConnectionHelper.Connect(DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);
            //Program.MakeInitialData();
        }
        void PopulatePlainCollection() {
            ConnectionHelper.Connect(DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);
            var uow = new UnitOfWork();
            ConnectionHelper.AddContact(uow, "FirstName0", 10);
            ConnectionHelper.AddContact(uow, "FirstName1", 20);
            uow.CommitChanges();
        }
        void PopulatePlainCollectionCrit() {
            ConnectionHelper.Connect(DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);
            var uow = new UnitOfWork();
            ConnectionHelper.AddContact(uow, "FirstName0", 10, false);
            ConnectionHelper.AddContact(uow, "FirstName0", 20, false);
            ConnectionHelper.AddContact(uow, "FirstName0", 30, true);
            ConnectionHelper.AddContact(uow, "FirstName0", 40, true);
            uow.CommitChanges();
        }
        void PopulateSelectFromCollection() {
            ConnectionHelper.Connect(DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);
            var uow = new UnitOfWork();
            var c0 = ConnectionHelper.AddContact(uow, "FirstName0");
            var t00 = ConnectionHelper.AddTask(uow, c0, "Task0-0", 10);
            var t01 = ConnectionHelper.AddTask(uow, c0, "Task0-1", 20);
            var c1 = ConnectionHelper.AddContact(uow, "FirstName1");
            var t10 = ConnectionHelper.AddTask(uow, c1, "Task1-0", 100);
            var t11 = ConnectionHelper.AddTask(uow, c1, "Task1-1", 200);   
            
            var c2 = ConnectionHelper.AddContact(uow, "FirstName2");
            var t20 = ConnectionHelper.AddTask(uow, c2, "Task2-0", 30);
            var t21 = ConnectionHelper.AddTask(uow, c2, "Task2-1", 40);
            var c3 = ConnectionHelper.AddContact(uow, "FirstName3");
            var t30 = ConnectionHelper.AddTask(uow, c3, "Task3-0", 200);
            var t31 = ConnectionHelper.AddTask(uow, c3, "Task3-1", 300);


            uow.CommitChanges();
        }
        [Test]
        public void AggregateOperandAvg_PlainCollection_1() {
            PopulatePlainCollection();
            CriteriaOperator criterion = CriteriaOperator.Parse("Avg([Age])");
            var uow = new UnitOfWork();
            var res = uow.Evaluate<Contact>(criterion, null);
            Assert.AreEqual(15, res);
        }
        [Test]
        public void AggregateOperandAvg_PlainCollection_2() {
            PopulatePlainCollection();
            CriteriaOperator criterion = new AggregateOperand(null, "Age", Aggregate.Avg);
            var uow = new UnitOfWork();
            var res = uow.Evaluate<Contact>(criterion, null);
            Assert.AreEqual(15, res);
        }
        [Test]
        public void AggregateOperandAvg_PlainCollection_3() {
            PopulatePlainCollection();
            CriteriaOperator criterion = CriteriaOperator.FromLambda<Contact, double>(x => FromLambdaFunctions.TopLevelAggregate<Contact>().Average(c => c.Age));
            var uow = new UnitOfWork();
            var res = uow.Evaluate<Contact>(criterion, null);
            Assert.AreEqual(15, res);
        }

        [Test]
        public void AggregateOperandAvg_PlainCollection_Crit_1() {
            PopulatePlainCollectionCrit();
            var crit = CriteriaOperator.Parse("Avg([Age])");
            var crit2 = CriteriaOperator.Parse("[IsActive]=true");
            var uow = new UnitOfWork();
            var res = uow.Evaluate<Contact>(crit, crit2);
            Assert.AreEqual(35, res);
        }
        [Test]
        public void AggregateOperandAvg_PlainCollection_Crit_2() {
            PopulatePlainCollectionCrit();
            var crit = CriteriaOperator.FromLambda<Contact, double>(x => FromLambdaFunctions.TopLevelAggregate<Contact>().Average(c => c.Age));
            var crit2 = CriteriaOperator.FromLambda<Contact>(x => x.IsActive);
            var uow = new UnitOfWork();
            var res = uow.Evaluate<Contact>(crit, crit2);
            Assert.AreEqual(35, res);
        }
        [Test]
        public void AggregateOperandAvg_PlainCollection_Crit_3() {
            PopulatePlainCollectionCrit();
            var crit = CriteriaOperator.Parse("Avg([Age])");
            var crit2 = CriteriaOperator.Parse("[IsActive]=true");
            var uow = new UnitOfWork();
            var res = uow.Evaluate<Contact>(crit, crit2);
            Assert.AreEqual(35, res);
        }


        [Test]
        public void AggregateOperandAvg_SelectFromCollection_1() {
            PopulateSelectFromCollection();
            var uow = new UnitOfWork();
            var crit = CriteriaOperator.Parse("[Tasks].Avg([Price])>100");
            var res = new XPCollection<Contact>(uow, crit).ToList();
            Assert.AreEqual(2, res.Count);
            Assert.AreEqual("FirstName1", res[0].FirstName);
            Assert.AreEqual("FirstName3", res[1].FirstName);
        }

        [Test]
        public void AggregateOperandAvg_SelectFromCollection_2() {
            PopulateSelectFromCollection();
            var uow = new UnitOfWork();
            var crit = new AggregateOperand("Tasks", "Price", Aggregate.Avg);
            var crit2 = new BinaryOperator(crit, 100, BinaryOperatorType.Greater);
            var res = new XPCollection<Contact>(uow, crit2).ToList();
            Assert.AreEqual(2, res.Count);
            Assert.AreEqual("FirstName1", res[0].FirstName);
            Assert.AreEqual("FirstName3", res[1].FirstName);
        }

        [Test]
        public void AggregateOperandAvg_SelectFromCollection_3() {
            PopulateSelectFromCollection();
            var uow = new UnitOfWork();
            var crit = CriteriaOperator.FromLambda<Contact>(x => x.Tasks.Average(t => t.Price) > 100);
            var res = new XPCollection<Contact>(uow, crit).OrderBy(x=>x.FirstName).ToList();
            Assert.AreEqual(2, res.Count);
            Assert.AreEqual("FirstName1", res[0].FirstName);
            Assert.AreEqual("FirstName3", res[1].FirstName);
        }

    }
}
