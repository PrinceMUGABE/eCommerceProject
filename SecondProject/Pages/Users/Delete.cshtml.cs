using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace SecondProject.Pages.Users
{
    
    public class DeleteModel : PageModel
    {
        public String errorMessage = "";
        public String successMessage = "";
        public List<UserInfo> userListInfo = new List<UserInfo>();
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=eCommerceProject;Integrated Security=True");
        UserInfo userInfo = new UserInfo();

        public void OnGet()
        {
            try 
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("exec displayUsers", con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    userInfo.id = reader.GetInt32(0).ToString();
                    userInfo.userName = reader.GetString(1);
                    userInfo.phoneNumber = reader.GetString(2);
                    userInfo.email = reader.GetString(3);
                    userInfo.password = reader.GetString(4);
                    userInfo.CreatedDate = reader.GetDateTime(5).ToString();
                    userListInfo.Add(userInfo);

                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        
        }
        public void OnPost()
        {
            userInfo.id = Request.Form["id"];
            userInfo.userName = Request.Form["username"];
            userInfo.email= Request.Form["email"];
            userInfo.password = Request.Form["password"];
            userInfo.phoneNumber = Request.Form["phone"];

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("exec deleteUserByPhone '"+userInfo.phoneNumber+"'",con);
                cmd.ExecuteNonQuery();
                successMessage = "User: "+userInfo.userName+"deleted successfully";
            }catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            Response.Redirect("/Users/BossView");
        }
    }
}
