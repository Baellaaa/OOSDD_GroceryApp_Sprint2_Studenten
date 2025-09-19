
namespace Grocery.Core.Models
{
    public partial class Client : Model
    {
        private string _emailAddress 
            get { return _emailAddress;} //property
            set{
                _emailAdresss = emailAdress
                }
        private string _password { get; set; } //property
        public Client(int id, string name, string emailAddress, string password) : base(id, name)
        {
            _emailAddress=emailAddress; 
            _password=password;
        }
    }
}
