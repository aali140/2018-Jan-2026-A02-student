using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HogWildSystem.ViewModels
{
    public class CustomerEditView
    {
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        //	Prov/State id.  Value will use a dropdown and the LookupView model
        public int? ProvStateID { get; set; }
        //	Country id.  Value will use a dropdown and the LookupView model
        public int? CountryID { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        //	Status id.  Value will use a dropdown and the LookupView model
        public int? StatusID { get; set; }
        public bool RemoveFromViewFlag { get; set; }
    }
}
