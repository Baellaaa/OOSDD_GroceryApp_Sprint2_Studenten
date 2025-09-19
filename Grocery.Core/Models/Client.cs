
namespace Grocery.Core.Models
{
    public partial class Client : Model
    {
        public string _emailAddress
        {
            get;
            private set;
        }
        public string _password 
        { 
            get;
            private set;
        }
        public Client(int id, string name, string emailAddress, string password) : base(id, name)
        {
            _emailAddress=emailAddress;
            _password=password;
        }
    }
}
