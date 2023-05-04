using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace SecondProject.Pages.Clients
{
    public class BossViewModel : PageModel
    {

        public List<ClientInfo> listClients = new List<ClientInfo>();

        public void OnGet()
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=eCommerceProject;Integrated Security=True");
            listClients.Clear();

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("exec displayClients", con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ClientInfo clientInfo = new ClientInfo();

                    clientInfo.id = reader.GetInt32(0).ToString();
                    clientInfo.name = reader.GetString(1);
                    clientInfo.phone = reader.GetString(2);
                    clientInfo.product_id = reader.GetInt32(3).ToString();
                    clientInfo.product_name = reader.GetString(4);
                    clientInfo.quantity = reader.GetInt32(5).ToString();
                    clientInfo.price = reader.GetString(6);
                    clientInfo.created_date = reader.GetDateTime(7).ToString();

                    listClients.Add(clientInfo);

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //return;
            }
        }
    }
}
