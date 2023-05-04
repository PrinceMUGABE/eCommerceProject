using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace SecondProject.Pages.Users
{
    public class EditModel : PageModel
    {
        public String errorMessage = "";
        public String successMessage = "";
        public UserInfo userInfos = new UserInfo();
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=eCommerceProject;Integrated Security=True");

        UserInfo userInfo = new UserInfo();

        public void OnGet()

        {
            //int id = Int32.Parse( Request.Form["id"]);
            String id = Request.Query["id"];
     
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("exec findUserbyId '" + Int32.Parse(id) + "'", con);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    userInfos.id = "" + reader.GetInt32(0).ToString();
                    userInfos.userName = reader.GetString(1);
                    userInfos.phoneNumber = reader.GetString(2);
                    userInfos.email = reader.GetString(3);
                    userInfo.password = reader.GetString(4);
                    userInfos.CreatedDate = reader.GetDateTime(5).ToString();
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
           
        }

        public void OnPost()

        {
            int id = Int32.Parse(Request.Form["id"]);
            userInfos.userName = Request.Form["username"];
            userInfos.phoneNumber = Request.Form["phone"];
            userInfos.email = Request.Form["email"];
            userInfos.password = Request.Form["passwd"];
            
           
            if (userInfos.userName.Length == 0 || userInfos.password.Length == 0 || userInfos.email.Length == 0
                                        || userInfos.phoneNumber.Length == 0)
            {
                errorMessage = "fill all the filds";
                return;
            }
            else if (userInfos.password.Length < 5)
            {
                errorMessage = "Password should be at least five characters";
                return;
            }
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("exec updateUserById '"+id+"','"+userInfos.userName+"','"+userInfos.phoneNumber+"','"+userInfos.email
                                                            +"','"+userInfos.password+"'",con);
                cmd.ExecuteNonQuery();
                successMessage = "User successfully updated";
            }catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            userInfos.userName = "";
            userInfos.phoneNumber = "";
            userInfos.email = "";
            userInfos.password = "";

            Response.Redirect("/Users/Index");
        }
    }
}
