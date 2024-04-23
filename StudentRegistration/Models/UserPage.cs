using Microsoft.Graph.Models;
using Newtonsoft.Json;

namespace StudentRegistration.Models
{
    public class UserPage
    {
        [JsonProperty("@odata.context")]
        public string ODataContext { get; set; }

        [JsonProperty("value")]
        public List<User> Value { get; set; }

        [JsonProperty("@odata.nextLink")]
        public string NextLink { get; set; }
    }
}
