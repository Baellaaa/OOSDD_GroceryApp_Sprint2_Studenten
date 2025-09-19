
using System.Net.Mail;

namespace Grocery.Core.Models
{
    public partial class Client : Model
    {
        public string _emailAddress { get; set; }
        public string _password { get; set; }
        public Client(int id, string name, string emailAddress, string password) : base(id, name)
        {
            _emailAddress = emailAddress;
            _password = password;
        }
    }
}

