using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kar_to_teka_2.Models;

namespace kar_to_teka_2.ViewModels
{
    public class EditCriminalViewModel
    {
        public Criminal Criminal { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Nickname { get; set; }
        public string Residence { get; set; }
        public string BirthPlace { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Wanted { get; set; }
    }
}
