using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VoBoo.Data;
using VoBoo.Models;

namespace VoBoo
{
    public class DictionaryModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DictionaryModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public DictionaryViewModel UserDictionary { get; set; }

        public class DictionaryViewModel
        {
            [Display(Name = "Dictionary name")]
            public string Name { get; set; }
            public string Description { get; set; }
            [Display(Name = "Learning language")]
            public string LearningLang { get; set; }
            [Display(Name = "Known language")]
            public string KnownLang { get; set; }
            [Display(Name = "Word-translation pairs")]
            public long WordTranslationPairCount { get; set; }
        }

        public async Task OnGetAsync(int id)
        {
            UserDictionary =  await _context.Dictionaries
                .Where(d => d.Id == id && d.User.UserName == User.Identity.Name)
                .Select(d => new DictionaryViewModel()
                {
                    Name = d.Name,
                    Description = d.Description,
                    LearningLang = d.LearningLang.Name,
                    KnownLang = d.KnownLang.Name,
                    WordTranslationPairCount = d.DictionaryTranslations.Count()
                })
                .FirstOrDefaultAsync();
        }
    }
}