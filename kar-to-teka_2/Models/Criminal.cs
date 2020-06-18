using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace kar_to_teka_2.Models
{
    public class Criminal : MongoBaseModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Nickname { get; set; }
        public string Residence { get; set; }
        public string BirthPlace { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Wanted { get; set; }
        public List<CommittedCrime> CommittedCrimes { get; set; }
    }
}
