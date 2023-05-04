using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace SecondProject.Pages.Users
{
    
    public class IndexModel : PageModel
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
            if(userInfos.userName.Length ==0 || userInfos.password.Length ==0)
            {
                errorMessage = "fill all the filds";
                return;
            }
            else if (userInfos.password.Length<5)
            {
                errorMessage = "Password should be at least five characters";
                return;
            }


            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("exec findUserByUsernameAndPassword '"+userInfos.userName+"','"+userInfos.password+"'", con);

                

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    String id = reader.GetInt32(0).ToString();
                    String usename = reader.GetString(1);
                    String email = reader.GetString(3);
                    String password = reader.GetString(4);
                    String createdAt = reader.GetDateTime(5).ToString();
                    string phone = reader.GetString(2);

                    if (usename.Equals("admin"))
                    {
                       
                        Response.Redirect("/Users/AdminView");
                    }
                    else if (usename.Equals("boss"))
                    {
                       
                        Response.Redirect("/Users/BossView");
                    }

                    successMessage = "welcome";

                }
                //errorMessage = "No such account, signup please";
             
            }


            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            userInfos.userName = "";
            userInfos.password = "";
           // Response.Redirect("/Products/Index");
        }
    }

    public class UserInfo
    {
        public String id;
        public string userName;
        public string password;
        public String email;
        public string phoneNumber;
        public String CreatedDate;
        
    }
}
