using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using dxTestSolutionXPO.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;

namespace dxTestSolutionXPO {
    class Program {
        static void Main(string[] args) {
            ConnectionHelper.Connect(DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);
            MakeInitialData();
            var session = new Session();
            var lst = new XPCollection<MyTask>(session);
            var cnt = lst.Count;
            var c = new XPCollection<Contact>(session)[0].Tasks.ToList();
        }

        static void MakeInitialData() {
            var session = new Session();
            var c = new XPCollection<Contact>(session).Count;
            if(c > 0) {
                return;
            }            
            for(int i = 0; i < 10; i++) {
                string contactName = "FirstName" + i;
                var contact = CreateObject<Contact>("FirstName", contactName, session);
                contact.LastName = "LastName" + i;
                contact.Age = i * 10;
                for(int j = 0; j < 5; j++) {
                    string taskName = "Subject" + i + " - " + j;
                    var task = CreateObject<MyTask>("Subject", taskName, session);
                    task.AssignedTo = contact;
                    task.Save();
                }
                contact.Save();
            }

        }
     static T CreateObject<T>(string propertyName, string value, Session session) {

            T theObject = session.FindObject<T>(new OperandProperty(propertyName) == value);
            if(theObject == null){
                theObject = (T)Activator.CreateInstance(typeof(T), new object[] { session });
                ((XPObject)(object)theObject).SetMemberValue(propertyName, value);
            }

            return theObject;

        }
    }
}
