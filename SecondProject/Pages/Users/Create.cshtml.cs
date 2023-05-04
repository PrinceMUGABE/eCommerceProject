using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace SecondProject.Pages.Users
{
    public class CreateModel : PageModel
    {

        public String errorMessage = "";
        public String successMessage = "";
        public UserInfo userInfos = new UserInfo();
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=eCommerceProject;Integrated Security=True");

        UserInfo userInfo = new UserInfo();
        public void OnGet()
        {
        }
        public void OnPost()
        {
            userInfos.userName = Request.Form["username"];
            userInfos.phoneNumber = Request.Form["phone"];
            userInfos.password = Request.Form["passwd"];
            userInfos.email = Request.Form["email"];

            if (userInfos.userName.Length == 0 || userInfos.password.Length == 0||userInfos.email.Length==0||
                                               userInfos.phoneNumber.Length==0)
            {
                errorMessage = "fill all the filds";
                return;
            }
             if (userInfos.phoneNumber.Length>10 || userInfos.phoneNumber.Length < 10)
            {
                errorMessage = "Provide correct Phone number";
                return;
            }

            try 
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("exec addUser '" + userInfos.userName + "','" + userInfos.phoneNumber + "','" +
                                                        userInfos.email + "','" + userInfos.password + "'", con);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            userInfos.userName = "";
            userInfos.phoneNumber = "";
            userInfos.password = "";
            userInfos.email = "";

            Response.Redirect("/Users/Index");
           
        }


    }
}
