using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace PhoneBook.Pages
{
    public class ContactsModel : PageModel
    {
        

        public List<Contacts> contactList = new List<Contacts>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.;Initial Catalog=PhoneBook;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Contacts ORDER BY Name ASC, Surname ASC, Created_at DESC";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Contacts contactInfo = new Contacts();
                                contactInfo.id = "" + reader.GetInt32(0);
                                contactInfo.name = reader.GetString(1);
                                contactInfo.surname = reader.GetString(2);
                                contactInfo.phone_number = "" + reader.GetInt32(3);
                                contactInfo.email = reader.GetString(4);
                                contactInfo.address = reader.GetString(5);
                                contactInfo.note = reader.GetString(6);
                                contactInfo.created_at = reader.GetDateTime(7).ToString();
                                contactInfo.favorite = reader.GetString(8);

                                contactList.Add(contactInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION: " + ex.ToString());
            }
        }
    }
    public class Contacts
    {
        public string id;
        public string name;
        public string surname;
        public string phone_number;
        public string email;
        public string address;
        public string note;
        public string created_at;
        public string favorite;
    }
}
