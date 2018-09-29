using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aerospike.Client;
using AerospikeAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AerospikeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TweetsController : ControllerBase
    {
        string nameSpace = "AirEngine";
        string setName = "Ayush";
        AerospikeClient aerospikeClient = new AerospikeClient("18.235.70.103", 3000);
        // GET: api/Tweets
        [HttpPut]
        public List<Record> Tweets([FromBody]List<string> IDs)
        {
            List<Record> records = new List<Record>();
            foreach (var id in IDs)
            {
                var key = new Key(nameSpace, setName, id.ToString());
                Record dataById = aerospikeClient.Get(new WritePolicy(), key);
                records.Add(dataById);
            }
            return records;
        }

        // PUT: api/Tweets/5
        [HttpPut]
        public void Put([FromBody]Tweet tweet)
        {
            aerospikeClient.Put(new WritePolicy(), new Key(nameSpace, setName, tweet.ID), new Bin[] { new Bin("" + tweet.Name, tweet.Value) });
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete]
        public void Delete([FromBody]string id)
        {
            aerospikeClient.Delete(new WritePolicy(), new Key(nameSpace, setName, id));
        }
    }
}
