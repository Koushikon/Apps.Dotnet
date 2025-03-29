using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models;

public class UserModel
{
    public int Id { get; set; }
    public int PersonId { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string StreetAddress { get; set; } = "";
    public string Ciy { get; set; } = "";
    public string State { get; set; } = "";
    public string ZipCode { get; set; } = "";
}
