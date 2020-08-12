using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebAppToBeSecured.Pages
{
    public class ProtectedPage : PageModel
    {
        private readonly ILogger<ProtectedPage> _logger;

        public ProtectedPage(ILogger<ProtectedPage> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
