using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.Diagnostics;

namespace dxTestSolutionXPO.Module.BusinessObjects {
     [DebuggerDisplay("Subject: {OrderItemName}")]
    public class OrderItem : XPObject { 
        public OrderItem(Session session)
            : base(session) {
        }
        public override void AfterConstruction() {
            base.AfterConstruction();
        }
        bool isAvailable;
        string _orderItemName;
        public string OrderItemName {
            get {
                return _orderItemName;
            }
            set {
                SetPropertyValue(nameof(OrderItemName), ref _orderItemName, value);
            }
        }
        Order _order;
        [Association]
        public Order Order {
            get {
                return _order;
            }
            set {
                SetPropertyValue(nameof(Order), ref _order, value);
            }
        }
        int _itemPrice;
        public int ItemPrice {
            get {
                return _itemPrice;
            }
            set {
                SetPropertyValue(nameof(ItemPrice), ref _itemPrice, value);
            }
        }
        
        public bool IsAvailable {
            get => isAvailable;
            set => SetPropertyValue(nameof(IsAvailable), ref isAvailable, value);
        }

    }
}