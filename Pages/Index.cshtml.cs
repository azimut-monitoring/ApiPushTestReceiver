using ApiPushTestReceiver.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ApiPushTestReceiver.Pages
{
    public class IndexPageModel : PageModel
    {
        public IEnumerable<string> Data { get; set; }

        public IActionResult OnGet()
        {
            Data = PushController.Items.ToArray();
            return Page();
        }
    }
}
