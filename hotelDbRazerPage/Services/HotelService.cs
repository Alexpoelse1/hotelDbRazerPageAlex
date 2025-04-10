using Microsoft.Data.SqlClient;
using hotelDbRazerPage.Interfaces;
using hotelDbRazerPage.Models;
using System.Data;
using Microsoft.IdentityModel.Tokens;
using System.Net;


namespace hotelDbRazerPage.Services
{
    public class HotelService : IHotelServiceAsync
    {
        private string connectionString = Secret.ConnectionString;
        private string queryString = "SELECT Hotel_No, Name, Address FROM Hotel";
        private string updateQuery = "UPDATE Hotel SET Name = @Name, Address = @Address WHERE Hotel_No = @Hotel_No";
        private string deleteQuery = "DELETE FROM Hotel WHERE Name=@Name;";
        private string insertQuery = "INSERT INTO Hotel (Hotel_No, Name, Address)\r\nVALUES (@Hotel_No, @Name, @Address);";

        private string searchNrQuery = "SELECT Hotel_No, Name, Address FROM Hotel WHERE Hotel_No = @Hotel_No";
        private string searchNameQuery = "SELECT Hotel_No, Name, Address FROM Hotel WHERE Name = @Name";
        private string searchAddressQuery = "SELECT Hotel_No, Name, Address FROM Hotels WHERE Address LIKE CONCAT('%', @ADDRESS, '%')";
        

        public HotelService()
        {
            
        }
        public async Task<List<Hotel>> Search(Search SearchI)
        {
            string search = SearchI.SearchText;
            string se_type=SearchI.SearchType;
            var hotels = new List<Hotel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    string query;

                    switch (se_type.ToLower())
                    {
                        case "number":
                            query = "SELECT TOP 5 * FROM Hotel WHERE Hotel_No LIKE @Search";
                            break;
                        case "name":
                            query = "SELECT TOP 5 * FROM Hotel WHERE Name LIKE @Search";
                            break;
                        case "address":
                            query = "SELECT TOP 5 * FROM Hotel WHERE Address LIKE @Search";
                            break;
                        default:
                            throw new ArgumentException("Invalid search type");
                    }

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Search", $"%{search}%");

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                hotels.Add(new Hotel
                                {
                                    Hotel_No = reader.GetInt32(reader.GetOrdinal("Hotel_No")),
                                    Name = reader.GetString(reader.GetOrdinal("Name")),
                                    Address = reader.GetString(reader.GetOrdinal("Address"))
                                });
                            }
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database error: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("General error: " + ex.Message);
                }
            }

            return hotels;
        }

        //

        public async Task<bool> CreateHotelAsync(Hotel hotel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(insertQuery, connection);

                    // Add parameters to prevent SQL injection
                    command.Parameters.AddWithValue("@Hotel_No", hotel.Hotel_No);
                    command.Parameters.AddWithValue("@Name", hotel.Name);
                    command.Parameters.AddWithValue("@Address", hotel.Address);


                    await connection.OpenAsync();
                    int rowsAffected = await command.ExecuteNonQueryAsync(); // Correct method for UPDATE

                    return true; // Return true if at least one row was Insert
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error: " + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("General error: " + ex.Message);
                }
            }
            return false; // Return false if the Insert fails
        }

        public async Task<Hotel> DeleteHotelAsync(int hotelNr)
        {
            Hotel hotel = await GetHotelFromIdAsync(hotelNr);
            //string updateQuery = "UPDATE Hotel SET HotelName = @HotelName, HotelAddress = @HotelAddress WHERE HotelNr = @HotelNr";              using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                    {
                        SqlCommand command = new SqlCommand(deleteQuery, connection);

                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@Name", GetHotelFromIdAsync(hotelNr).Result.Name);
                        

                        await connection.OpenAsync();
                        int rowsAffected = await command.ExecuteNonQueryAsync(); // Correct method for UPDATE

                        return hotel; // Return true if at least one row was Deleted
                    }
                    catch (SqlException sqlExp)
                    {
                        Console.WriteLine("Database error: " + sqlExp.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("General error: " + ex.Message);
                    }
                }
                return null; // Return false if the Delete fails
            }

        public async Task<List<Hotel>> GetAllHotelAsync()
        {
            List<Hotel> hoteller = new List<Hotel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                {
                    try
                    {
                        SqlCommand command = new SqlCommand(queryString, connection);
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        Thread.Sleep(50);
                        while (await reader.ReadAsync())
                        {
                            int hotelNr = reader.GetInt32("Hotel_No");
                            string hotelNavn = reader.GetString("Name");
                            string hotelAdr = reader.GetString("Address");
                            Hotel hotel = new Hotel(hotelNr, hotelNavn, hotelAdr);
                            hoteller.Add(hotel);
                        }
                        reader.Close();
                    }
                    catch (SqlException sqlExp)
                    {
                        Console.WriteLine("Database error" + sqlExp.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Generel fejl: " + ex.Message);
                    }
                    finally
                    {

                    }
                }
                return hoteller;
            }
        }

        public async Task<Hotel> GetHotelFromIdAsync(int hotelNum)
        {
            Hotel result_hotel = new Hotel();
            List<Hotel> hoteller = new List<Hotel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                {
                    try
                    {
                        SqlCommand command = new SqlCommand(queryString, connection);
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        Thread.Sleep(1000);
                        while (await reader.ReadAsync())
                        {
                            int hotelNr = reader.GetInt32("Hotel_No");
                            string hotelNavn = reader.GetString("Name");
                            string hotelAdr = reader.GetString("Address");
                            if (hotelNr == hotelNum)
                            {
                                result_hotel = new Hotel(hotelNr, hotelNavn, hotelAdr);
                            }

                        }
                        reader.Close();
                    }
                    catch (SqlException sqlExp)
                    {
                        Console.WriteLine("Database error" + sqlExp.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Generel fejl: " + ex.Message);
                    }
                    finally
                    {

                    }
                }
                return result_hotel;
            }
        }

        public async Task<List<Hotel>> GetHotelsByNameAsync(string name)
            {


            List<Hotel> hoteller = new List<Hotel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                {
                    try
                    {
                        SqlCommand command = new SqlCommand(queryString, connection);
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        Thread.Sleep(1000);
                        while (await reader.ReadAsync())
                        {
                            int hotelNr = reader.GetInt32("Hotel_No");
                            string hotelNavn = reader.GetString("Name");
                            string hotelAdr = reader.GetString("Address");
                            if (hotelNavn == name)
                            {
                                Hotel hotel = new Hotel(hotelNr, hotelNavn, hotelAdr);
                                hoteller.Add(hotel);
                            }


                        }
                        reader.Close();
                    }
                    catch (SqlException sqlExp)
                    {
                        Console.WriteLine("Database error" + sqlExp.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Generel fejl: " + ex.Message);
                    }
                    finally
                    {
                                             }
                    }
                return hoteller;
            }
        }
        
        //
        public async Task<bool> UpdateHotelAsync(Hotel hotel, int hotelNum)
        {
            //string updateQuery = "UPDATE Hotel SET HotelName = @HotelName, HotelAddress = @HotelAddress WHERE HotelNr = @HotelNr";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(updateQuery, connection);

                    // Add parameters to prevent SQL injection
                    command.Parameters.AddWithValue("@Name", hotel.Name);
                    command.Parameters.AddWithValue("@Address", hotel.Address);
                    command.Parameters.AddWithValue("@Hotel_No", hotelNum);

                    await connection.OpenAsync();
                    int rowsAffected = await command.ExecuteNonQueryAsync(); // Correct method for UPDATE

                    return rowsAffected > 0; // Return true if at least one row was updated
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error: " + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("General error: " + ex.Message);
                }
            }
            return false; // Return false if the update fails
        }


        //


    }
}
