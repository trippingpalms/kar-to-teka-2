using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kar_to_teka_2.Models
{
    public class CommittedCrime
    {
        public ObjectId _id;
        public Crime Crime { get; set; }
        public string Description { get; set; }
        public DateTime ImprisonmentDate { get; set; }
        public int ImprisonmentLength { get; set; }

        public CommittedCrime()
        {
            _id = ObjectId.GenerateNewId();
        }
    }
}
