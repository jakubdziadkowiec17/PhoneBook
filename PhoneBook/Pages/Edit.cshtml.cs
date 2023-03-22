using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace PhoneBook.Pages
{
    public class EditModel : PageModel
    {
        public Contacts contactInfo = new Contacts();
        public String errorMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];
            try
            {
                String connectionString = "Data Source=.;Initial Catalog=PhoneBook;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Contacts WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                contactInfo.id = "" + reader.GetInt32(0);
                                contactInfo.name = reader.GetString(1);
                                contactInfo.surname = reader.GetString(2);
                                contactInfo.phone_number = "" + reader.GetInt32(3);
                                contactInfo.email = reader.GetString(4);
                                contactInfo.address = reader.GetString(5);
                                contactInfo.note = reader.GetString(6);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            contactInfo.id = Request.Form["id"];
            contactInfo.name = Request.Form["name"];
            contactInfo.surname = Request.Form["surname"];
            contactInfo.phone_number = Request.Form["phone_number"];
            contactInfo.email = Request.Form["email"];
            contactInfo.address = Request.Form["address"];
            contactInfo.note = Request.Form["note"];

            if (contactInfo.name.Length == 0 || contactInfo.surname.Length == 0 || contactInfo.phone_number.Length == 0)
            {
                errorMessage = "Wszystkie pola sa wymagane!";
                return;
            }

            try
            {
                String connectionString = "Data Source=.;Initial Catalog=PhoneBook;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE Contacts " +
                                 "SET name=@name, surname=@surname, phone_number=@phone_number, email=@email, address=@address, note=@note " +
                                 "WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", contactInfo.name);
                        command.Parameters.AddWithValue("@surname", contactInfo.surname);
                        command.Parameters.AddWithValue("@email", contactInfo.email);
                        command.Parameters.AddWithValue("@phone_number", contactInfo.phone_number);
                        command.Parameters.AddWithValue("@address", contactInfo.address);
                        command.Parameters.AddWithValue("@note", contactInfo.note);
                        command.Parameters.AddWithValue("@id", contactInfo.id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Contacts");
        }
    }
}
