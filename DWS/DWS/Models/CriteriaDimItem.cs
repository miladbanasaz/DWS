using System.Collections.Generic;

namespace DWS.Models
{
    public class CriteriaDimItem
    {
        public string Criteria { get; set; }
        public IEnumerable<string> Dims { get; set; }
    }
}