using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kar_to_teka_2.Models;

namespace kar_to_teka_2.ViewModels
{
    public class AddCriminalPhotoViewModel
    {
        public Criminal Criminal { get; set; }
        public byte[] Image { get; set; }
    }
}
