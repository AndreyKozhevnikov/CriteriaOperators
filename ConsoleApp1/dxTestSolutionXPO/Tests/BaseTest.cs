using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dxTestSolutionXPO.Tests {
    public class BaseTest {
        public void PopulatePlainCollection() {
            ConnectionHelper.Connect(DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);
            var uow = new UnitOfWork();
            ConnectionHelper.AddOrder(uow, "FirstName0", 10);
            ConnectionHelper.AddOrder(uow, "FirstName1", 20);
            uow.CommitChanges();
        }
        public void PopulateСollectionWithActive() {
            ConnectionHelper.Connect(DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);
            var uow = new UnitOfWork();
            ConnectionHelper.AddOrder(uow, "FirstName0", 10, false);
            ConnectionHelper.AddOrder(uow, "FirstName0", 20, false);
            ConnectionHelper.AddOrder(uow, "FirstName0", 30, true);
            ConnectionHelper.AddOrder(uow, "FirstName0", 40, true);
            ConnectionHelper.AddOrder(uow, "FirstName0", 50, true);
            uow.CommitChanges();
        }

        public void PopulateSelectFromCollection() {
            ConnectionHelper.Connect(DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);
            var uow = new UnitOfWork();
            var c0 = ConnectionHelper.AddOrder(uow, "FirstName0");
            var t00 = ConnectionHelper.AddOrderItem(uow, c0, "Task0-0", 10);
            var t01 = ConnectionHelper.AddOrderItem(uow, c0, "Task0-1", 20);
            var c1 = ConnectionHelper.AddOrder(uow, "FirstName1");
            var t10 = ConnectionHelper.AddOrderItem(uow, c1, "Task1-0", 100);
            var t11 = ConnectionHelper.AddOrderItem(uow, c1, "Task1-1", 200);

            var c2 = ConnectionHelper.AddOrder(uow, "FirstName2");
            var t20 = ConnectionHelper.AddOrderItem(uow, c2, "Task2-0", 30);
            var t21 = ConnectionHelper.AddOrderItem(uow, c2, "Task2-1", 40, 55);
            var c3 = ConnectionHelper.AddOrder(uow, "FirstName3");
            var t30 = ConnectionHelper.AddOrderItem(uow, c3, "Task3-0", 300);
            var t31 = ConnectionHelper.AddOrderItem(uow, c3, "Task3-1", 400);
            uow.CommitChanges();
        }
        public void PopulateSelectFromCollectionForCount() {
            ConnectionHelper.Connect(DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);
            var uow = new UnitOfWork();
            var c0 = ConnectionHelper.AddOrder(uow, "FirstName0");
            var t00 = ConnectionHelper.AddOrderItem(uow, c0, "Task0-0", 10);
            var c1 = ConnectionHelper.AddOrder(uow, "FirstName1");
            var c2 = ConnectionHelper.AddOrder(uow, "FirstName2");
            var t20 = ConnectionHelper.AddOrderItem(uow, c2, "Task2-0", 10);
            var t21 = ConnectionHelper.AddOrderItem(uow, c2, "Task2-0", 20);
            uow.CommitChanges();
        }
        public void PopulateSimpleCollectionForMaxMin() {
            ConnectionHelper.Connect(DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);
            var uow = new UnitOfWork();
            var c0 = ConnectionHelper.AddOrder(uow, "FirstName0", 44);
            var t00 = ConnectionHelper.AddOrderItem(uow, c0, "Item0-1", 10, false);
            var t01 = ConnectionHelper.AddOrderItem(uow, c0, "Item0-1", 20, true);
            var t02 = ConnectionHelper.AddOrderItem(uow, c0, "Item0-1", 30, true);
            var t03 = ConnectionHelper.AddOrderItem(uow, c0, "Item0-1", 40, false);
            uow.CommitChanges();
        }

        public void PopulateDiffItems() {
            ConnectionHelper.Connect(DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);
            var uow = new UnitOfWork();
            var c0 = ConnectionHelper.AddOrder(uow, "FirstName0");
            var t00 = ConnectionHelper.AddOrderItem(uow, c0, "Task0-0", 10);
            var t01 = ConnectionHelper.AddOrderItem(uow, c0, "Task0-1", 20);
            var c4 = ConnectionHelper.AddOrder(uow, "FirstName4");
            var t40 = ConnectionHelper.AddOrderItem(uow, c4, "Task4-0", 300);
            var t41 = ConnectionHelper.AddOrderItem(uow, c4, "Task4-1", 400);
            var t42 = ConnectionHelper.AddOrderItem(uow, c4, "Task4-2", 456);
            var t43 = ConnectionHelper.AddOrderItem(uow, c4, "Task4-3", 456);
            uow.CommitChanges();
        }
        public void PopulateOrderItemsWithSameValues() {
            ConnectionHelper.Connect(DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);
            var uow = new UnitOfWork();
            var c0 = ConnectionHelper.AddOrder(uow, "FirstName0");
            var t00 = ConnectionHelper.AddOrderItem(uow, c0, "Task0-0", 10);
            var t01 = ConnectionHelper.AddOrderItem(uow, c0, "Task0-1", 10);
            var t02 = ConnectionHelper.AddOrderItem(uow, c0, "Task0-2", 20);
            uow.CommitChanges();
        }
        public void PopulateComplexCollection() {
            ConnectionHelper.Connect(DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);
            var uow = new UnitOfWork();
            var c0 = ConnectionHelper.AddOrder(uow, "FirstName0");
            var t00 = ConnectionHelper.AddOrderItem(uow, c0, "Task0-0");
            var t01 = ConnectionHelper.AddOrderItem(uow, c0, "Task0-1");

            var c1 = ConnectionHelper.AddOrder(uow, "FirstName1");
            var t10 = ConnectionHelper.AddOrderItem(uow, c1, "Task1-0");

            var c2 = ConnectionHelper.AddOrder(uow, "FirstName2");
            var t20 = ConnectionHelper.AddOrderItem(uow, c2, "Task2-0");
            var t21 = ConnectionHelper.AddOrderItem(uow, c2, "Task2-1");




            uow.CommitChanges();
        }
        public void PopulateComplexCollectionWithAvailable() {
            ConnectionHelper.Connect(DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);
            var uow = new UnitOfWork();

            var c0 = ConnectionHelper.AddOrder(uow, "FirstName0");
            var t00 = ConnectionHelper.AddOrderItem(uow, c0, "Task0-0", 10);
            var t01 = ConnectionHelper.AddOrderItem(uow, c0, "Task0-1", 20);

            var c1 = ConnectionHelper.AddOrder(uow, "FirstName1");
            var t10 = ConnectionHelper.AddOrderItem(uow, c1, "Task1-0", 100, true);
            var t11 = ConnectionHelper.AddOrderItem(uow, c1, "Task1-1", 200);

            var c2 = ConnectionHelper.AddOrder(uow, "FirstName2");
            var t20 = ConnectionHelper.AddOrderItem(uow, c2, "Task2-0", 30, true);
            var t21 = ConnectionHelper.AddOrderItem(uow, c2, "Task2-1", 40);
            uow.CommitChanges();
        }

    }
}
