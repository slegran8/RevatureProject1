using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ShanesRestaurantApp.Models
{

    public class RestaurantModel
    {
        public int mealId { get; set; }
        public int loginId { get; set; }

        public string orderName { get; set; }
        public string mealCategory { get; set; }

        public string mealName { get; set; }

        public float mealPrice { get; set; }

        public bool mealIsOrdered { get; set; }

        SqlConnection con = new SqlConnection("server= DESKTOP-NVMDB78\\SHANEINSTANCE; database= RestaurantDB; integrated security=true");

        public List<RestaurantModel> GetMealsList()
        {
            SqlCommand cmd_allMeals = new SqlCommand("select * from restaurantMeal", con);
            List<RestaurantModel> list = new List<RestaurantModel>();
            SqlDataReader mealReader = null;
            try
            {
                con.Open();
                mealReader = cmd_allMeals.ExecuteReader();

                while (mealReader.Read())
                {
                    list.Add(new RestaurantModel()
                    {
                        mealId = Convert.ToInt32(mealReader[0]),
                        loginId = Convert.ToInt32(mealReader[1]),
                        orderName = Convert.ToString(mealReader[2]),
                        mealCategory = Convert.ToString(mealReader[3]),
                        mealName = Convert.ToString(mealReader[4]),
                        mealPrice = Convert.ToSingle(mealReader[5]),
                        mealIsOrdered = Convert.ToBoolean(mealReader[6]),
                    });
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                mealReader.Close();
                con.Close();
            }
            return list;
        }

        public List<RestaurantModel> MealListByOrderName(string orderName)
        {
            SqlCommand cmd_allMeals = new SqlCommand("select * from restaurantMeal WHERE orderName Like '%' + @oName + '%' ", con);
            cmd_allMeals.Parameters.AddWithValue("oName", orderName);
            List<RestaurantModel> ordered_list = new List<RestaurantModel>();
            SqlDataReader mealReader = null;
            try
            {
                con.Open();
                mealReader = cmd_allMeals.ExecuteReader();

                while (mealReader.Read())
                {
                    ordered_list.Add(new RestaurantModel()
                    {
                        mealId = Convert.ToInt32(mealReader[0]),
                        loginId = Convert.ToInt32(mealReader[1]),
                        orderName = Convert.ToString(mealReader[2]),
                        mealCategory = Convert.ToString(mealReader[3]),
                        mealName = Convert.ToString(mealReader[4]),
                        mealPrice = Convert.ToSingle(mealReader[5]),
                        mealIsOrdered = Convert.ToBoolean(mealReader[6]),
                    });

                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                mealReader.Close();
                con.Close();
            }
            return ordered_list;
        }

        public List<RestaurantModel> MealListByLoginID(int loginId)
        {
            SqlCommand cmd_allMeals = new SqlCommand("select * from restaurantMeal WHERE loginMatch = @loginID", con);
            cmd_allMeals.Parameters.AddWithValue("loginID", loginId);
            List<RestaurantModel> ordered_list = new List<RestaurantModel>();
            List<RestaurantModel> blank_list = new List<RestaurantModel>();
            SqlDataReader mealReader = null;
            try
            {
                con.Open();
                mealReader = cmd_allMeals.ExecuteReader();

                while (mealReader.Read())
                {
                    ordered_list.Add(new RestaurantModel()
                    {
                        mealId = Convert.ToInt32(mealReader[0]),
                        loginId = Convert.ToInt32(mealReader[1]),
                        orderName = Convert.ToString(mealReader[2]),
                        mealCategory = Convert.ToString(mealReader[3]),
                        mealName = Convert.ToString(mealReader[4]),
                        mealPrice = Convert.ToSingle(mealReader[5]),
                        mealIsOrdered = Convert.ToBoolean(mealReader[6]),
                        
                    });

                    
                }
            }
            

            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                mealReader.Close();
                con.Close();
            }

            if (ordered_list == null)
            {
                throw new Exception("ERROR");
            }
            return ordered_list;
        }


        public string OrderMeal(RestaurantModel newMealOrder)
        {
            SqlCommand cmd_addOrder = new SqlCommand("insert into restaurantMeal values(@mLogin,@oName,@mCategory,@mName,@mPrice,@mIsOrdered)", con);
            
            cmd_addOrder.Parameters.AddWithValue("mLogin", newMealOrder.loginId);
            cmd_addOrder.Parameters.AddWithValue("oName", newMealOrder.orderName);
            cmd_addOrder.Parameters.AddWithValue("mCategory", newMealOrder.mealCategory);
            cmd_addOrder.Parameters.AddWithValue("mName", newMealOrder.mealName);
            cmd_addOrder.Parameters.AddWithValue("mPrice", newMealOrder.mealPrice);
            cmd_addOrder.Parameters.AddWithValue("mIsOrdered", newMealOrder.mealIsOrdered);
            

            try
            {
                con.Open();
                cmd_addOrder.ExecuteNonQuery();
            }
            catch (Exception es)
            {
                throw new Exception(es.Message);

            }
            finally
            {
                con.Close();
            }
            return "Item Added!";
        }


        public string DeleteMeal(int mealID)
        {
            SqlCommand cmd_remove = new SqlCommand("Delete from restaurantMeal WHERE mealId = @mID", con);
            cmd_remove.Parameters.AddWithValue("@mID", mealID);
            try
            {
                con.Open();
                cmd_remove.ExecuteNonQuery();
            }
            catch (Exception es)
            {
                throw new Exception(es.Message);
            }
            finally
            {
                con.Close();
            }
            return "Item Removed Successfully";

        }
    }
       
    
}
