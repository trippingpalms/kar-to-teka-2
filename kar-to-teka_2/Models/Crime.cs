using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kar_to_teka_2.Models
{
    public class Crime : MongoBaseModel
    {
        public int Paragraph { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
