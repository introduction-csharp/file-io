using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileIOConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // grab the stations once - cache them in a list 
            IList<string> stations = AllTheStations();

            // loop over a list of target strings 
            foreach(string t in new List<string> {  "mackarel", "piranha", "bacteria", "sturgeon", "St Pauls School", "John Colet Day"})
            {                
                IList<string> hits = SharesNoLettersWith(t, stations);
                /* using the string formatting, how many hits and then a comma-separated of 
                 * those which don't match
                 */ 
                Console.WriteLine("{0} - [{1}]: {2}", t, hits.Count, string.Join(", ", hits));
            }
            // this isn't VS.Code/dotnet.Core so read the key 
            Console.ReadKey();
        }

        /// <summary>
        /// Gets the data from the stations next file, returning just the station names 
        /// </summary>
        /// <returns>returns an IList&lt;string&gt; interface not a List. More on interfaces later in the C# course</returns>
        static IList<string> AllTheStations() 
        {
            return 
                File
                    .ReadAllText("stations.txt")        // HACK: hard-coded filename 
                    .Split('\n')                        // split on the the newline character 
                    .Select(l => l.Trim())              // returns items with any whitespace removed from the beginning and end of each line 
                    .Where(l => l.Contains(","))        // filters out any lines that have no commas 
                    .Select(l => l.Split(',')[0])       // split on commas (no station name has commas) and return the first item 
                    .ToList();                          // gather all the responses into a list 
        }

        static bool DoesNotShareLettersWith(string thing, string target)
        {
            return 
                thing
                    .All(t => !target.Contains(t));     // checks that *all* the characters in 'thing' are not contained in 'target' 
        }

        static IList<string> SharesNoLettersWith(string target, IList<string> stations)
        {
            return 
                stations
                    // filters out any for which the boolean returned is true 
                    // uses .ToLowerInvariant to convert to lower case in a more general case than your local culture setting might give: 
                    // https://stackoverflow.com/questions/6225808/string-tolower-and-string-tolowerinvariant 
                    .Where(s => DoesNotShareLettersWith(s.ToLowerInvariant(), target.ToLowerInvariant()))
                    .ToList();
        }
    }
}
