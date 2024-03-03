using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ProiectBD.Pages.Login
{
    public class LogincshtmlModel : PageModel
    {
        [BindProperty]
        public LoginModel Input { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (Input.Username == "admin" && Input.Password == "admin")
            {
                return Redirect("/Meniu/Meniu");
            }
            else return Page();
        }


        public class LoginModel
        {
            [Required]
            [Display(Name = "User Name")]
            public string Username { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
    }
}

