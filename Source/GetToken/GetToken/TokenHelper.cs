using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thinktecture.IdentityModel.Client;

namespace TokenHelper
{
    public class TokenProvider
    {

        private string tokenErrorMessage = null;
        public TokenProvider() { }
        public Token GetToken(string username, string password, string scope, out string errorMessage)
        {
            Token token = default(Token);
            token = GetTokenAsync(username, password, scope).GetAwaiter().GetResult();
            errorMessage = tokenErrorMessage;
            return token;
        }
        public async Task<Token> GetTokenAsync(string username, string password, string scope)
        {
            tokenErrorMessage = string.Empty;

            OAuth2Client client = CreateClient();

            TokenResponse token = await client.RequestResourceOwnerPasswordAsync(username, password, scope);
            if (token.IsError)
            {
                tokenErrorMessage = string.Format("SSO Error: {0}", token.Error);
                return default(Token);
            }
            else if (token.IsHttpError)
            {
                tokenErrorMessage = string.Format("SSO Error: {0} - {1}", token.HttpErrorStatusCode, token.HttpErrorReason);
                return default(Token);
            }

            return new Token(DateTime.Now.AddSeconds(token.ExpiresIn), token.AccessToken);
        }

        private OAuth2Client CreateClient()
        {
            //authority - is the URL of the STS server
            //cliendID - is the client string for the system you are trying to access
            //clientSecret - repeat the clientId in this field
            //NOTE : do not use Base64 encoded string, use the actual string. THe IdentityModel library will do the conversion

            string authority = "https://sts.nordpoolgroupppe.com";

            string clientId = "client_dayahead_api";
            string clientSecret = "client_dayahead_api";

            return new OAuth2Client(
                new Uri(authority + "/connect/token"),
                clientId,
                clientSecret);
        }
        public struct Token
        {
            private readonly DateTime expiry;
            private readonly string accessToken;

            public Token(DateTime expiry, string accessToken)
            {
                this.expiry = expiry;
                this.accessToken = accessToken;
            }

            public DateTime Expiry
            {
                get { return expiry; }
            }

            public string AccessToken
            {
                get { return accessToken; }
            }
        }

    }
}
