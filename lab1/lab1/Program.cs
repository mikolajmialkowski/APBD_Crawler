//s20635 Mikolaj Mialkowski 
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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
                    string zawartoszStrony = await response.Content.ReadAsStringAsync(); // await dla a-synch
                    String pattern = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

                    Regex regex = new(pattern, RegexOptions.IgnoreCase); // bez uwzglednienia rozmiaru liter

                    MatchCollection mathColection = regex.Matches(zawartoszStrony);

                    HashSet<String> hashSet = new HashSet<String>();

                    foreach (Match match in mathColection)
                        hashSet.Add(match.Value);

                    if (hashSet.Count >= 1)
                        foreach (string str in hashSet)
                            Console.WriteLine(str);
                    else
                        Console.WriteLine("Nie znaleziono adrsów e-mail");
                }
            }
            catch (Exception e) {
                Console.WriteLine("Błąd podczs pobierania strony: " + e.Message);
            }
            finally {
                httpClient.Dispose();
            }
        }
    }
}
