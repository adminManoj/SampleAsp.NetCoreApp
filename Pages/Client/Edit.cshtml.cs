using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SampleWebApp.Model;

namespace SampleWebApp.Pages.Client
{
    public class EditModel : PageModel
    {
        public User user = new User();
        public string errorMessage = String.Empty;
        public string successMessage = String.Empty;

        private readonly IConfiguration _configuration;
        public EditModel(IConfiguration _configuration)
        {
            this._configuration = _configuration;
        }
        public void OnGet()
        {
            string id = Request.Query["id"];

            try
            {
                DAL dal = new DAL();
                user = dal.GetUser(id, _configuration);
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            user.Id = Request.Form["id"];
            user.FirstName = Request.Form["FirstName"];
            user.LastName = Request.Form["LastName"];

            if (user.FirstName.Length == 0 || user.LastName.Length == 0)
            {
                errorMessage = "All fields are required.";
                return;
            }

            try
            {
                DAL dal = new DAL();
                int i = dal.UpdateUser(user, _configuration);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            user.FirstName = ""; user.LastName = "";
            successMessage = "User updated.";
            Response.Redirect("/Client/Index");
        }
    }
}
