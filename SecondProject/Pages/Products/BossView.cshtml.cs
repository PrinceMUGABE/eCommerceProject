using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace SecondProject.Pages.Products
{
    public class BossViewModel : PageModel
    {

        public List<ProductInfo> listproducts = new List<ProductInfo>();
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=eCommerceProject;Integrated Security=True");
        public ProductInfo productInfo = new ProductInfo();

        public void OnGet()
        {

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("exec getProducts", con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    productInfo = new ProductInfo();
                    productInfo.product_id = reader.GetInt32(0).ToString();
                    productInfo.proct_name = reader.GetString(1);
                    productInfo.product_price = reader.GetString(2);
                    productInfo.created_at = reader.GetDateTime(3).ToString();
                    listproducts.Add(productInfo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



    }


    
    
}
