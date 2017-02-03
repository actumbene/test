using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenHelper;
using static TokenHelper.TokenProvider;


namespace ConsoleTestToken
{
    class Program
    {
        static void Main(string[] args)
        {


            string PersonalUsernameDA = "API_TEST_ENERGI_DANMARK";
            string PersonalPasswordDA = "K8Dnd#cmoGI890w3";

            string scope = "dayahead_api";

            TokenProvider tHelper = new TokenProvider();

            string err = null;
            Token tok = tHelper.GetToken(PersonalUsernameDA, PersonalPasswordDA, scope, out err);

            string errMsg = string.IsNullOrEmpty(err) ? "OK" : err;

            Console.WriteLine("Res: " + errMsg);

            //var task = tHelper.GetTokenAsync(PersonalUsernameDA, PersonalPasswordDA, scope);
            //task.Wait();
            //var res = task.Result;
            Console.ReadLine();
        }

    }
}
