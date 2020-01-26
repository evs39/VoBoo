using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VoBoo.Data;
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
            public string Name { get; set; }
            public string Description { get; set; }
            [Required]
            public string LearningLang { get; set; }
            [Required]
            public string KnownLang { get; set; }
        }

        public async Task OnGetAsync()
        {
            AllowedLanguages = await _context.Languages.Select(l => l.Name).ToListAsync();
        }
    }
}