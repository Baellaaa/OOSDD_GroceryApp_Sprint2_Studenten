using Grocery.Core.Helpers;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IClientService _clientService;
        public AuthService(IClientService clientService)
        {
            _clientService = clientService;
        }
        public Client? Login(string email, string password)
        {

            Client? client = _clientService.Get(email); //searches for email in repository

            if (client == null) //not found in repository, will return null
            {
                return null;
            }
            bool passwordCheck = PasswordHelper.VerifyPassword(password, client._password); //.Password instead of passwordFromClient? propbably needs to be the input
            return passwordCheck ? client : null; //if true, returns client, false returns null

                //Vraag de klantgegevens [Client] op die je zoekt met het opgegeven emailadres
                //Als je een klant gevonden hebt controleer dan of het password matcht --> PasswordHelper.VerifyPassword(password, passwordFromClient)
                //Als alles klopt dan klantgegveens teruggeven, anders null
        }
    }
}
