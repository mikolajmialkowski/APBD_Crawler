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
                throw new ArgumentNullException("Pass URL adress");

            string url = args[0];
            using (HttpClient httpClient = new HttpClient()) // automatycznie uruchomi dispose, troche jak finnaly{} --> httpClient.Dispose();
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode) {
                    string zawartoszStrony = await response.Content.ReadAsStringAsync();
                    String pattern = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

                    Regex regex = new Regex(pattern, RegexOptions.IgnoreCase); // bez uwzglednienia rozmiaru liter

                    MatchCollection mathColection = regex.Matches(zawartoszStrony);

                    HashSet<String> hashSet = new HashSet<String>();

                    foreach (Match match in mathColection)
                        hashSet.Add(match.Value);

                    foreach (string str in hashSet) {
                        Console.WriteLine(str);

                    }



                    //potrzebny fix regexa

                }
            }
        }
    }
}
