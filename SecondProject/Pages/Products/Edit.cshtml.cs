using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace SecondProject.Pages.Products
{
    public class EditModel : PageModel
    {
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=eCommerceProject;Integrated Security=True");
        public String errorMessage = "";
        public String successmessage = "";
        public ProductInfo ProductInfo = new ProductInfo();

        


        public void OnGet()
        {
            String id = Request.Query["id"];
            

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("exec findProductById '"+Int32.Parse(id)+"'", con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    ProductInfo = new ProductInfo();
                    ProductInfo.product_id = reader.GetInt32(0).ToString();
                    ProductInfo.proct_name = reader.GetString(1);
                    ProductInfo.product_price = reader.GetString(2);
                    ProductInfo.created_at = reader.GetDateTime(3).ToString();


                }

            }
            catch(Exception ex)
            {
                errorMessage= ex.Message;
                return;
            }

            
        }

        public void OnPost()
        {
            int id = Int32.Parse(Request.Form["id"]);  
            ProductInfo.proct_name = Request.Form["name"];
            ProductInfo.product_price = Request.Form["price"];
            




            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("exec updateProductById '" + id + "', '" + ProductInfo.proct_name+"','"+ProductInfo.product_price+ "' ", con);
                cmd.ExecuteNonQuery();

                successmessage = "Product: " + ProductInfo.proct_name + " successfull updated";

            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            
            ProductInfo.product_id = "";
            ProductInfo.proct_name = "";
            ProductInfo.product_price = "";
            ProductInfo.created_at = "";

            Response.Redirect("/Products/Index");
        }
    }
}
