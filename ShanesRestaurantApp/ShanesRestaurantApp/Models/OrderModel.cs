using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Serilog;
using Microsoft.Extensions.Logging;


namespace ShanesRestaurantApp.Models
{
    public class OrderModel
    {
        public int orderId { get; set; }
        public int loginMatchID { get; set; }
        public float totalAmount { get; set; }
        public bool mealIsOrdered { get; set; }


        SqlConnection con = new SqlConnection("server= DESKTOP-NVMDB78\\SHANEINSTANCE; database= RestaurantDB; integrated security=true");

        public string FinalizeMeal(int loginId)
        {
            //insert into orderTable (loginMatchID, totalAmount, mealIsOrdered) select 4 , Sum(mealPrice),0 FROM restaurantMeal WHERE loginMatch = 4 AND mealIsOrdered = 0
            SqlCommand cmd_FinalizeMeal = new SqlCommand("insert into orderTable (loginMatchID, totalAmount, mealIsOrdered) select @loginID , Sum(mealPrice), 1 FROM restaurantMeal WHERE loginMatch = @loginID AND mealIsOrdered = 0", con);
            SqlCommand cmd_ChangeStatus= new SqlCommand("update restaurantMeal set mealIsOrdered = 1 WHERE loginMatch = @loginID AND mealIsOrdered = 0", con);
            cmd_FinalizeMeal.Parameters.AddWithValue("@loginID", loginId);
            cmd_ChangeStatus.Parameters.AddWithValue("@loginID", loginId);
            try
            {
                con.Open();
                cmd_FinalizeMeal.ExecuteNonQuery();
                cmd_ChangeStatus.ExecuteNonQuery();
            }
            catch (Exception es)
            {
                throw new Exception(es.Message);

            }
            finally
            {
                con.Close();
            }
            return "Meal Ordered!";
        }


        public List<OrderModel> GetOrderList()
        {
            SqlCommand cmd_allOrders = new SqlCommand("select * from orderTable", con);
            List<OrderModel> list = new List<OrderModel>();
            SqlDataReader orderReader = null;
            try
            {
                con.Open();
                orderReader = cmd_allOrders.ExecuteReader();

                while (orderReader.Read())
                {
                    list.Add(new OrderModel()
                    {
                        orderId = Convert.ToInt32(orderReader[0]),
                        loginMatchID = Convert.ToInt32(orderReader[1]),
                        totalAmount = Convert.ToSingle(orderReader[2]),
                        mealIsOrdered = Convert.ToBoolean(orderReader[3]),
                        
                    });
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                orderReader.Close();
                con.Close();
            }
            return list;
        }

        public List<OrderModel> OrderListByLoginID(int loginId)
        {
            SqlCommand cmd_allOrders = new SqlCommand("select * from orderTable WHERE loginMatchID = @loginID", con);
            cmd_allOrders.Parameters.AddWithValue("loginID", loginId);
            List<OrderModel> ordered_list = new List<OrderModel>();
            SqlDataReader orderReader = null;

            try
            {
                con.Open();
                orderReader = cmd_allOrders.ExecuteReader();

                while (orderReader.Read())
                {
                    ordered_list.Add(new OrderModel()
                    {
                        orderId = Convert.ToInt32(orderReader[0]),
                        loginMatchID = Convert.ToInt32(orderReader[1]),
                        totalAmount = Convert.ToSingle(orderReader[2]),
                        mealIsOrdered = Convert.ToBoolean(orderReader[3]),
                        
                    });
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                orderReader.Close();
                con.Close();
            }
            return ordered_list;
        }

        //public string OrderMeal(int loginId)
        //{
        //    SqlCommand cmd_OrderMeal = new SqlCommand("", con);
        //}
    }
}
