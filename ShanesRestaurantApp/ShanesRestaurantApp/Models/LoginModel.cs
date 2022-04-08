using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ShanesRestaurantApp.Models
{
    public class LoginModel
    {
        public int loginID { get; set; }
        public string username { get; set; }
        public string loginPassword { get; set; }

        public bool isAdmin { get; set; }

        SqlConnection con = new SqlConnection("server= DESKTOP-NVMDB78\\SHANEINSTANCE; database= RestaurantDB; integrated security=true");

        public string CreateLogin(LoginModel newLogin)
        {
            SqlCommand cmd_addLogin = new SqlCommand("insert into LoginTable (username, loginPassword) VALUES(@uName,@lPassword)", con);

            cmd_addLogin.Parameters.AddWithValue("uName", newLogin.username);
            cmd_addLogin.Parameters.AddWithValue("lPassword", newLogin.loginPassword);
            try
            {
                con.Open();
                cmd_addLogin.ExecuteNonQuery();
            }
            catch (Exception es)
            {
                throw new Exception(es.Message);

            }
            finally
            {
                con.Close();
            }
            return "Account Created!";
        }

        public List<LoginModel> GetLoginList()
        {
            SqlCommand cmd_allOrders = new SqlCommand("select * from LoginTable", con);
            List<LoginModel> list = new List<LoginModel>();
            SqlDataReader orderReader = null;
            try
            {
                con.Open();
                orderReader = cmd_allOrders.ExecuteReader();

                while (orderReader.Read())
                {
                    list.Add(new LoginModel()
                    {
                        loginID = Convert.ToInt32(orderReader[0]),
                        username = Convert.ToString(orderReader[1]),
                        loginPassword = Convert.ToString(orderReader[2]),
                        isAdmin = Convert.ToBoolean(orderReader[3]),

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

        public LoginModel GetLoginUserName(int loginId)
        {
            SqlCommand cmd_loginUsername = new SqlCommand("select * from LoginTable WHERE loginID = @loginId", con);
            cmd_loginUsername.Parameters.AddWithValue("loginId", loginId);
            LoginModel loginModel = new LoginModel();
            try
            {
                con.Open();
                SqlDataReader login_reader = cmd_loginUsername.ExecuteReader();
                if (login_reader.Read())
                {
                    loginModel.loginID = loginId;
                    loginModel.username = login_reader.GetString(1);
                    loginModel.loginPassword = login_reader.GetString(2);
                    loginModel.isAdmin = login_reader.GetBoolean(3);
                } else {
                    throw new Exception("User Not Found");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return loginModel;
        }

    }
}
