using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SampleWebApp.Model;

namespace SampleWebApp.Pages.Client
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public IndexModel(IConfiguration _configuration)
        {
            this._configuration = _configuration;
        }
        public List<User> listUsers = new List<User>(); 
        public void OnGet()
        {
            DAL dal = new DAL();
            listUsers = dal.GetUsers(_configuration);
        }
    }
}
