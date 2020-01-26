using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VoBoo.Data;
using VoBoo.Extensions;
using VoBoo.Models;

namespace VoBoo
{
    public class AddLanguageModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public InputModel Input { get; set; }

        public AddLanguageModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public class InputModel
        {
            public string Name { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var language = await _context.Languages.FirstOrDefaultAsync(l => l.Name.ToLower().Trim() == Input.Name.ToLower().Trim());
            if (language != null)
            {
                ModelState.AddModelError(string.Empty, "");
                return Page();
            }

            language = new Language()
            {
                Name = Input.Name.EachSentenceWordTrimAndOnlyFirstCharToUpper()
            };

            _context.Languages.Add(language);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Languages");
        } 
    }
}