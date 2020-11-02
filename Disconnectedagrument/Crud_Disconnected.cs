using System;
using System.Data;
using System.Data.SqlClient;



namespace Disconnectedagrument
{
    class Crud_Disconnected
    {
        static void Main()
        {
            SqlConnection con = null;
            SqlDataAdapter da;
            try
            {
                con = new SqlConnection("data source=DESKTOP-HQU5381\\SQLEXPRESS;initial catlog=Northwind;integrated security userid=sa Password=newuser123#");
                da = new SqlDataAdapter("select *from region", con);

                con.Open();
                DataSet ds = new DataSet();
                da.Fill(ds, "NorthwindRegion");
                DataTable dt = ds.Tables["NorthwindREgion"];

                foreach (DataRow row in dt.Rows)
                {
                    foreach (DataColumn col in dt.Columns)
                    {
                        Console.WriteLine(row[col]);
                        Console.WriteLine("");

                    }
                    Console.WriteLine(" ");
                }
                SqlCommandBuilder scb = new SqlCommandBuilder(da);
                da.Fill(ds);
                //*Adding one more table*//
                Console.WriteLine("-------------");
                da = new SqlDataAdapter("select *from shippers", con);
                da.Fill(ds, "Northwindshipper");
                dt = ds.Tables["Northwindshipper"];//northwind database//
                foreach (DataRow row in dt.Rows)
                {
                    foreach (DataColumn col in dt.Columns)
                    {
                        Console.WriteLine(row[col]);
                        Console.WriteLine("");
                    }
                    Console.WriteLine(" ");
                }

            catch (Exception)
            }
            
            
        }
    }
}
            
  

