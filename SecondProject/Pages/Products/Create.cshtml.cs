using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace SecondProject.Pages.Products
{
    public class CreateModel : PageModel
    {
        
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=eCommerceProject;Integrated Security=True");
        public String errorMessage = "";
        public String successmessage = "";
        public ProductInfo productInfo = new ProductInfo();


        

        public void OnGet()
        {
        }

        public void OnPost()
        {
            
            productInfo.proct_name = Request.Form["name"];
            productInfo.product_price = Request.Form["price"];
            try
            {con.Open();
                SqlCommand cmd = new SqlCommand("exec addProduct '"+productInfo.proct_name+"','"+productInfo.product_price+"'",con);
                cmd.ExecuteNonQuery();
                

            }catch (Exception ex)
            {
                errorMessage= ex.Message;
            }
            successmessage = "Product: " + productInfo.proct_name + " saved successfully";
            productInfo.proct_name = "";
            productInfo.product_price = "";

            Response.Redirect("/Products/Index");
        }



    }
}
