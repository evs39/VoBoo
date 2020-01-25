using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoBoo.Models
{
    public class Word
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long LanguageId { get; set; }

        public virtual Language Language { get; set; }
    }
}
