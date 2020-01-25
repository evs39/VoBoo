using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoBoo.Models
{
    public class UserDictionary
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public long LearningLangId { get; set; }
        public long? KnownLangId { get; set; }

        public virtual IEnumerable<DictionaryTranslation> DictionaryTranslations { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Language LearningLang { get; set; }
        public virtual Language KnownLang { get; set; }
    }
}