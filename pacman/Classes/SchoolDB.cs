using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dbhandler
{
    class SchoolDB
    {
        #region fields
        public MySqlConnection _connection = new MySqlConnection("Server=localhost;Database=cruddb;Uid=root;Pwd=;");
        #endregion

        #region methods/functions
        public bool CreateUser(string naam)
        {
            try
            {
                _connection.Open();
                string query = "INSERT INTO `name` (`id`, `fullname`) VALUES (NULL, @naam);";
                MySqlCommand command = new MySqlCommand(query, _connection);
                command.Parameters.AddWithValue("@naam", naam);

                int count = Convert.ToInt32(command.ExecuteScalar());

                return true; 

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;

            }
            finally
            {
                _connection.Close();
                GetUserId(naam);
            }
        }
        public bool InsertScore(int score, string name)
        {
            bool success = false;

            try
            {
                _connection.Open();

                // Check if the user exists and get the corresponding ID
                int userId = GetUserId(name);

                if (userId != 0)
                {
                    string query = "INSERT INTO `crud` (`id`, `score`) VALUES (@id, @score);";
                    MySqlCommand command = new MySqlCommand(query, _connection);
                    command.Parameters.AddWithValue("@id", userId);
                    command.Parameters.AddWithValue("@score", score);

                    int nrOfRowsAffected = command.ExecuteNonQuery();
                    success = (nrOfRowsAffected != 0);
                }
                else
                {
                    MessageBox.Show("User does not exist. Name: " + name); // Debugging statement
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }

            return success;
        }




        public int GetUserId(string naam)
        {
            _connection.Close();
            _connection.Open();

            MySqlCommand command = _connection.CreateCommand();
            command.CommandText = "SELECT id FROM `name` WHERE fullname = @naam;";
            command.Parameters.AddWithValue("@naam", naam);

            int userId = 0; 

            
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    userId = reader.GetInt32(0); 
                }
            }

            _connection.Close();

            MessageBox.Show(userId.ToString());
            return userId;
        }

    }

    
}

#endregion