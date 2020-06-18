using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kar_to_teka_2.Models;

namespace kar_to_teka_2.ViewModels
{
    public class AssignCrimeViewModel
    {
        public Criminal Criminal { get; set; }
        public Crime Crime { get; set; }
        public string Description { get; set; }
        public DateTime ImprisonmentDate { get; set; }
        public int ImprisonmentLength { get; set; }
    }
}
