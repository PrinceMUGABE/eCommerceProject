using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace SecondProject.Pages.Users
{
    public class BossViewModel : PageModel
    {
        public List<UserInfo> listUsers = new List<UserInfo>();
        public void OnGet()
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=eCommerceProject;Integrated Security=True");
            listUsers.Clear();
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
                    listUsers.Add(userInfo);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
  
}

