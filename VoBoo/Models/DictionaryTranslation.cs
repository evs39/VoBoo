using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoBoo.Models
{
    public class DictionaryTranslation
    {
        public long Id { get; set; }
        public long UserDictionaryId { get; set; }
        public long WordTranslationId { get; set; }

        public virtual UserDictionary UserDictionary { get; set; }
        public virtual WordTranslation WordTranslation { get; set; }
    }
}