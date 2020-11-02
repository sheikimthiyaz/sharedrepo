using System;
using System.Data.SqlClient;
using System.Transactions;
using System.Configuration;


using System.Text;
using System.Threading.Tasks;

namespace Disconnectedagrument
{


namespace TransactionScopeEg
    {
        class Local_DistributedTransactionEg
        {
            static void LocalTransactionScope()
            {
                /*using{

                }*/
                int i, j;
                SqlTransaction mytran = null;
                string myconnectionString = ("Data Source= DESKTOP-HQU5381\\SQLEXPRESS;Initial Catalog=Northwind;User ID=sa;Password=newuser123#");
                Console.WriteLine("connection established with SQL server");
                //transaction
                using (TransactionScope myscope = new TransactionScope())
                {
                    //connection
                    using (SqlConnection conn = new SqlConnection(myconnectionString))
                    {
                        conn.Open();
                        try
                        {
                            //*begin transction
                            mytran = conn.BeginTransaction();

                            SqlCommand mycommand2 = new SqlCommand("Insert into Shippers values('MyShop','94915364')", conn);
                            j = mycommand2.ExecuteNonQuery();
                            Console.WriteLine("Inserted record in Shipper table:{0}", j);
                            var mycommand = new SqlCommand("Insert into Region values(9,'southwest')", conn);
                            i = mycommand.ExecuteNonQuery();
                            //*Commit transaction
                            mytran.Commit();
                            //* myscope.Complete();
                        }
                        catch (Exception e)
                        {
                            if (mytran != null)
                            {
                                mytran.Rollback();

                                Console.WriteLine(e);
                                Console.WriteLine("not complete");
                            }
                        }
                    }
                }
            }
            static void DistributedTransactionScope()
            {
                string myconnectionString = ("Data Source=DESKTOP-HQU5381\\SQLEXPRESS;Initial Catalog=Northwind;User ID=sa;Password=newuser123#");
                string myconnectionString1 = ("Data Source= DESKTOP-HQU5381\\SQLEXPRESS;Initial Catalog=dbEmployee873;User ID=sa;Password=newuser123#");
                using (TransactionScope myscope = new TransactionScope())
                {
                    using (var myconn = new SqlConnection(myconnectionString))
                    {
                        myconn.Open();
                        var mycommand2 = new SqlCommand("Insert into Shippers values('vegetableshop','1254789')", myconn);
                        mycommand2.ExecuteNonQuery();
                        using (var myconn1 = new SqlConnection(myconnectionString1))
                        {
                            myconn1.Open();
                            var cmd = new SqlCommand("Insert into Book values('B005','AAAA')", myconn1);
                            cmd.ExecuteNonQuery();
                        }

                    }
                    myscope.Complete();
                }

            }
            static void Main()
            {
                try
                {
                    LocalTransactionScope();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                Console.Read();
            }

        }

    }
}

