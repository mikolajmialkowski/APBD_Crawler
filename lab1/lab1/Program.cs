//s20635 Mikolaj Mialkowski 
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Crawler {
    public class Program {
        public static async Task Main(string[] args) {

            if (args.Length == 0)
                throw new ArgumentNullException("Pass URL address");

            string url = args[0];

            if (!(Uri.IsWellFormedUriString(url,UriKind.Absolute)))
                throw new ArgumentException("Pass correct URL address");

            HttpClient httpClient = new();

            try {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                

                if (response.IsSuccessStatusCode) {
                    string siteContent = await response.Content.ReadAsStringAsync(); // await for a-sync
                    String pattern = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

                    Regex regex = new(pattern, RegexOptions.IgnoreCase); //ignore letter case

                    MatchCollection mathCollection = regex.Matches(siteContent);

                    HashSet<string> hashSet = new HashSet<string>();

                    foreach (Match match in mathCollection)
                        hashSet.Add(match.Value);

                    if (hashSet.Count >= 1)
                        foreach (string str in hashSet)
                            Console.WriteLine(str);
                    else
                        Console.WriteLine("Haven't found e-mail addresses");
                }
            }
            catch (Exception e) {
                Console.WriteLine("Error during site downland: " + e.Message);
            }
            finally {
                httpClient.Dispose();
            }
        }
    }
}
