using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoBoo.Models
{
    public class WordTranslation
    {
        public long Id { get; set; }
        public long WordId { get; set; }
        public long TranslationId { get; set; }

        public virtual Word Word { get; set; }
        public virtual Word Translation { get; set; }
    }
}
