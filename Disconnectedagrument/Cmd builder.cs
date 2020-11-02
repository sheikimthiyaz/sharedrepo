
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Disconnectedagrument
{
    class Cmd_builder
    {
        static void Main()
        {
            //2. create sql connect object
            //connction string
            SqlConnection con = null;
            SqlDataAdapter da;

            try
            {
                //Sql server authentication
                con = new SqlConnection(
                "Data Source= LAPTOP-2JJFD34B;Initial Catalog=Northwind;User ID=sa;Password=newuser123#");
                da = new SqlDataAdapter("select * from Region", con);

                con.Open();
                DataSet ds = new DataSet();
                da.Fill(ds, "NorthWindRegion");
                DataTable dt = ds.Tables["NorthwindRegion"];
                foreach (DataRow row in dt.Rows)
                {
                    foreach (DataColumn col in dt.Columns)
                    {
                        Console.WriteLine(row[col]);
                        Console.Write(" ");
                    }
                    Console.WriteLine("  ");
                }
                //Adding one more table
                Console.WriteLine("---------------------");
                da = new SqlDataAdapter("select * from Shippers", con);
                da.Fill(ds, "NOrthwindShippers");
                dt = ds.Tables["NOrthwindShippers"];
                foreach (DataRow row in dt.Rows)
                {
                    foreach (DataColumn col in dt.Columns)
                    {
                        Console.WriteLine(row[col]);
                        Console.Write(" ");
                        //Console.WriteLine("".PadLeft(20,'='));
                    }
                    Console.WriteLine("  ");
                }
                //updating record Region table by adding new Row in the dataset
                SqlCommandBuilder scb = new SqlCommandBuilder(da);
                da.Fill(ds);
                //adding new row
                DataRow row = ds.Tables["NorthwindRegion"].NewRow();
                row["RegionID"] = 10;
                row["regionDescription"] = "NW";
                //adding row to datatable in dataset
                ds.Tables["NorthwindRegion"].Rows.Add(row);

                ds.UpdateCommand = scb.GetUpdateCommand();
                da.Update(ds);
                Console.WriteLine("---------------");
                dt = ds.Tables["NorthwindRegion"];


            }

            catch (Exception e)
            {
                Console.WriteLine(e);
            }



        }
    }
}
   
