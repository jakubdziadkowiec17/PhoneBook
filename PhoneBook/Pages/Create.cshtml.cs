using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace PhoneBook.Pages
{
    public class CreateModel : PageModel
    {
        public Contacts contactInfo = new Contacts();
        public String errorMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
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

            //zapisanie do bazy

            try
            {
                String connectionString = "Data Source=.;Initial Catalog=PhoneBook;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Contacts " + "(Name, Surname, Phone_number, Email, Address, Note, Favorite) VALUES" + "(@name, @surname, @phone_number, @email, @address, @note, 'no');";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", contactInfo.name);
                        command.Parameters.AddWithValue("@surname", contactInfo.surname);
                        command.Parameters.AddWithValue("@email", contactInfo.email);
                        command.Parameters.AddWithValue("@phone_number", contactInfo.phone_number);
                        command.Parameters.AddWithValue("@address", contactInfo.address);
                        command.Parameters.AddWithValue("@note", contactInfo.note);

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
