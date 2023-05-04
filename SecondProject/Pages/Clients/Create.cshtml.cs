using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace SecondProject.Pages.Clients
{
    public class CreateModel : PageModel
    {
       public  SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=eCommerceProject;Integrated Security=True");
        
        public String errorMessage = "";
        public String successMessage = "";
        public ClientInfo ClientInfo = new ClientInfo();
        public ProductInfo ProductInfo = new ProductInfo();
        public int total;
        public int totali;
        public String thisTotal;
        public String amount;

        public List<ProductInfo> listProducts()
        {
            List<ProductInfo> allproduct = new List<ProductInfo>();
            con.Open();
            SqlCommand cmd1 = new SqlCommand("exec getProducts", con);
            SqlDataReader reader = cmd1.ExecuteReader();
            while (reader.Read())
            {
                ProductInfo productInfo = new ProductInfo();
                productInfo.product_id = reader.GetInt32(0).ToString();
                productInfo.proct_name = reader.GetString(1);
                productInfo.product_price = reader.GetString(2);
                productInfo.created_at = reader.GetDateTime(3).ToString();
                allproduct.Add(productInfo);
            }
            return allproduct;
        }

       
        public void OnGet()
        {
            CreateModel mo=new CreateModel();

              mo.listProducts();
            //String amount = thisTotal;

        }
        public void OnPost()
        {
            

            ClientInfo.name = Request.Form["name"];
            ClientInfo.phone = Request.Form["phone"];
           // ClientInfo.product_id = Request.Form["prod_Id"];
            ClientInfo.product_name = Request.Form["prod_Name"];
            ClientInfo.quantity = Request.Form["quantity"];
            ClientInfo.price = Request.Form["price"];

            con.Open();
            SqlCommand cmd2 = new SqlCommand("exec findProductByname'" + ClientInfo.product_name + "'", con);

            //SqlCommand cmd3 = new SqlCommand("",con);
            SqlDataReader reader = cmd2.ExecuteReader();
            ProductInfo productInfo = new ProductInfo();
            while (reader.Read())
            {
                productInfo.product_id = reader.GetInt32(0).ToString();
                productInfo.proct_name = reader.GetString(1);
                productInfo.product_price = reader.GetString(2);
                productInfo.created_at = reader.GetDateTime(3).ToString();
               

            }
            




            if (ClientInfo.name.Length == 0 || ClientInfo.phone.Length == 0  || ClientInfo.quantity.Length == 0)
            {
                errorMessage = "fill all the field";
                return;
            }


            try
            {

                totali = Int32.Parse(ClientInfo.quantity) * Int32.Parse(productInfo.product_price);
                thisTotal = totali.ToString();

                SqlCommand cmd = new SqlCommand("exec saveCLient '" + ClientInfo.name + "','" + ClientInfo.phone + "','" + productInfo.product_id + "','" +
                        productInfo.proct_name + "','" + ClientInfo.quantity + "','" + thisTotal + "'", con);
                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                successMessage = ex.Message;
                return;
            }
            ClientInfo.name = "";
            ClientInfo.phone = "";
            ClientInfo.quantity = "";
            ClientInfo.price = "";

        }
    }
    public class ProductInfo
    {
        public String product_id;
        public String proct_name;
        public String product_price;
        public String created_at;

    }
}
