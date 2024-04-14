using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models;

public class Contact
{
    public Contact()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        PhoneNumber = [];   // Or, new List<string>();
        EmailAddress = [];  // Or, new List<string>();
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<string> PhoneNumber { get; set; }
    public List<string> EmailAddress { get; set; }
}