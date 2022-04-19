using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.Diagnostics;

namespace dxTestSolutionXPO.Module.BusinessObjects {
   [DebuggerDisplay("FirstName: {FirstName}")]
    public class Contact : XPObject { 
        public Contact(Session session)
            : base(session) {
        }
        public override void AfterConstruction() {
            base.AfterConstruction();
        }
       string firstName;
        public string FirstName {
            get {
                return firstName;
            }
            set {
                SetPropertyValue(nameof(FirstName), ref firstName, value);
            }
        }
        string lastName;
        public string LastName {
            get {
                return lastName;
            }
            set {
                SetPropertyValue(nameof(LastName), ref lastName, value);
            }
        }
		int age;
        public int Age {
            get {
                return age;
            }
            set {
                SetPropertyValue(nameof(Age), ref age, value);
            }
        }

        bool isActive;
        public bool IsActive {
            get {
                return isActive;
            }
            set {
                SetPropertyValue("IsActive", ref isActive, value);
            }
        }
        // DateTime _birthDate;
        // public DateTime BirthDate {
        // get {
        // return _birthDate;
        // }
        // set {
        // SetPropertyValue(nameof(BirthDate), ref _birthDate, value);
        // }
        // }	
        //[EditorAlias(EditorAliases.RichTextPropertyEditor)]
        //public byte[] Text { get; set; }		
        [Association("Contact-Tasks")]
        public XPCollection<MyTask> Tasks {
            get {
                return GetCollection<MyTask>(nameof(Tasks));
            }
        }


    }
}