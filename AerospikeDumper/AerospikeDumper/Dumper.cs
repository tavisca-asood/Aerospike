using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aerospike.Client;
using CsvHelper;
using Microsoft.VisualBasic.FileIO;

namespace AerospikeDumper
{
    class Dumper
    {
        static void Main(string[] args)
        {
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\tweets.csv";
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    CsvReader csvreader = new CsvReader(reader);
                    List<Tweet> tweets = csvreader.GetRecords<Tweet>().ToList();
                    tweets.RemoveRange(20000, tweets.Count - 20000);
                    string nameSpace = "AirEngine";
                    string setName = "Ayush";
                    AerospikeClient aerospikeClient = new AerospikeClient("18.235.70.103", 3000);
                    foreach (Tweet tweet in tweets)
                    {
                        Key key = new Key(nameSpace, setName, tweet.id);
                        aerospikeClient.Put(new WritePolicy(), key, new Bin[] {
                         new Bin("text", tweet.text),
                         new Bin("favorited",tweet.favorited),
                         new Bin("favoriteCount",tweet.favoriteCount),
                         new Bin("created",tweet.created),
                         new Bin("truncated",tweet.truncated),
                         new Bin("id",tweet.id),
                         new Bin("statusSource",tweet.statusSource),
                         new Bin("screenName",tweet.screenName),
                         new Bin("retweetCount",tweet.retweetCount),
                         new Bin("isRetweet",tweet.isRetweet),
                         new Bin("timestamp",tweet.timestamp),
                         new Bin("date",tweet.date)
                        });
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.StackTrace);
                throw;
            }
        }
    }
}
