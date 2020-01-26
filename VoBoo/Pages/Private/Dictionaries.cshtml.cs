using System;
using System.Collections.Generic;
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
    public class DictionariesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IList<UserDictionary> UserDictionaries { get; set; }

        public DictionariesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            UserDictionaries = await _context.Dictionaries.Where(d => d.User.UserName == HttpContext.User.Identity.Name).ToListAsync();
        }
    }
}