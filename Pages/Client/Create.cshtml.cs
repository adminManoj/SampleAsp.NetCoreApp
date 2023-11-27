using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SampleWebApp.Model;

namespace SampleWebApp.Pages.Client
{
    public class CreateModel : PageModel
    {
        public User user = new User();
        public string errorMessage = String.Empty;
        public string successMessage = String.Empty;

        private readonly IConfiguration _configuration;
        public CreateModel(IConfiguration _configuration)
        {
            this._configuration = _configuration;
        }
        public void OnGet()
        {
        }

        public void OnPost()
        {
            user.FirstName = Request.Form["FirstName"];
            user.LastName = Request.Form["LastName"];

            if(user.FirstName.Length == 0 || user.LastName.Length == 0)
            {
                errorMessage = "All fields are required.";
                return;
            }

            try
            {
                DAL dal = new DAL();
                int i = dal.AddUser(user, _configuration);
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            user.FirstName = ""; user.LastName = "";
            successMessage = "New User added.";
            Response.Redirect("/Client/Index");
        }
    }
}
