using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknikprogramlama.Configuration.Manager.Abstract;
using Teknikprogramlama.Configuration.Manager.OAuth;

namespace AppConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var oauth = OAuthSection.Open();

            // Setting Up Github Client
            oauth.Github = new Github
            {
                ClientId = "d069de44bba1d5faab57",
                ClientSecret = "96beb02bc4cea08737bd066773fe2f4903abf9dc",
                Scope = "user, user:email, user:follow, gist, read:org"
            };
            oauth.Save("OAuth");

            oauth = OAuthSection.Open();
            Console.WriteLine(oauth.Github.Scope);
            Console.ReadLine();
        }
    }
}
