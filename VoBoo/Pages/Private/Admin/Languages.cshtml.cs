using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VoBoo.Data;

namespace VoBoo
{
    public class LanguagesModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        
        public IList<string> LanguageNames { get; set; }

        public LanguagesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            LanguageNames = await _context.Languages.Select(l => l.Name).ToListAsync();
        }
    }
}