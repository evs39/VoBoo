using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VoBoo.Attributes.Validation;
using VoBoo.Data;
using VoBoo.Extensions;
using VoBoo.Models;

namespace VoBoo
{
    public class AddDictionaryModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AddDictionaryModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<string> AllowedLanguages { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Dictionary name")]
            public string Name { get; set; }
            public string Description { get; set; }
            [Required]
            [Display(Name ="Learning language")]
            public string LearningLang { get; set; }
            [Required]
            [Display(Name = "Known language")]
            [Unlike("LearningLang")]
            public string KnownLang { get; set; }
        }

        public async Task OnGetAsync()
        {
            AllowedLanguages = await _context.Languages.Select(l => l.Name).ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var dictionary = await _context.Dictionaries
                    .FirstOrDefaultAsync(d => d.Name.ToLower().Trim() == Input.Name.ToLower().Trim()
                        && d.User.UserName == User.Identity.Name);
                if (dictionary != null)
                    return await ReturnIfError("Dictionary name already occupied.");

                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
                if (user == null)
                    return await ReturnIfError("User not found.");
                
                var languages = await _context.Languages.Where(l => l.Name == Input.KnownLang
                    || l.Name == Input.LearningLang).ToListAsync();
                if (languages.Count < 2)
                    return await ReturnIfError("Not all languages found");

                dictionary = new UserDictionary()
                {
                    User = user,
                    Name = Input.Name.EachSentenceWordTrimAndOnlyFirstCharToUpper(),
                    Description = Input.Description,
                    KnownLang = languages.FirstOrDefault(l => l.Name == Input.KnownLang),
                    LearningLang = languages.FirstOrDefault(l => l.Name == Input.LearningLang)
                };

                _context.Dictionaries.Add(dictionary);
                await _context.SaveChangesAsync();
                return RedirectToPage("/private/dictionaries");
            }

            return await ReturnIfError();
        }

        public async Task<IActionResult> ReturnIfError(string errorName = null)
        {
            if (errorName != null)
                ModelState.AddModelError("", errorName);

            await OnGetAsync();
            return Page();
        }
    }
}