using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace SecondProject.Pages.Users
{
    public class AdminEditorPageModel : PageModel
    {
        public String errorMessage = "";
        public String successMessage = "";
        public List<UserInfo> listuserInfos = new List<UserInfo>();
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=eCommerceProject;Integrated Security=True");
        

        public void OnGet()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("exec displayUsers", con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    UserInfo userInfo = new UserInfo();
                    userInfo.id = reader.GetInt32(0).ToString();
                    userInfo.userName = reader.GetString(1);
                    userInfo.phoneNumber = reader.GetString(2);
                    userInfo.email = reader.GetString(3);
                    userInfo.password = reader.GetString(4);
                    userInfo.CreatedDate = reader.GetDateTime(5).ToString();
                    listuserInfos.Add(userInfo);

                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

        }
        public void OnPost()
        {
            UserInfo userInfo = new UserInfo();
            userInfo.id = Request.Form["id"];
            userInfo.userName = Request.Form["username"];
            userInfo.email = Request.Form["email"];
            userInfo.password = Request.Form["password"];
            userInfo.phoneNumber = Request.Form["phone"];

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("exec deleteUserByUsername '" + userInfo.userName + "'", con);
                cmd.ExecuteNonQuery();
                successMessage = "User: " + userInfo.userName + "deleted successfully";
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            Response.Redirect("/Users/Delete");
        }
    }
}

