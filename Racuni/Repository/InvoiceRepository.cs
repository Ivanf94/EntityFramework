using System.Data;
using Microsoft.Data.SqlClient;
using Racuni.Models;


namespace Racuni.Repository
{
    public class InvoiceRepository
    {
        private string _connectionString;
        public InvoiceRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("Invoices");
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new Exception("Connection string not valid!!");
            }
        }

        public InvoiceRepository(string conn)
        {
            _connectionString = conn;
        }

        public List<Invoice> GetInvoices()
        {
            List<Invoice> invoices = new List<Invoice>();

            try
            {
                SqlConnection connection = new SqlConnection(_connectionString);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM Invoices";

                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Invoice inv = new Invoice();
                    inv.InvoiceNumber = (int)reader["InvoiceNumber"];
                    inv.DateOfIssue = (DateTime)reader["DateOfIssue"];
                    inv.InvoiceItems = GetInvoiceItemsByInvoiceNumber(inv.InvoiceNumber);

                    invoices.Add(inv);
                }

                reader.Close();
                connection.Close();
                connection.Dispose();
            }
            catch(Exception ex)
            {
                
            }
            return invoices;
        }

        public List<InvoiceItem> GetInvoiceItemsByInvoiceNumber(int invoiceNumber)
        {
            List<InvoiceItem> items = new List<InvoiceItem>();
            try
            {
                SqlConnection connection = new SqlConnection(_connectionString);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM InvoiceItem WHERE InvoiceNumber=@invoice_num";
                cmd.Parameters.AddWithValue("@invoice_num", invoiceNumber);

                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    InvoiceItem item = new InvoiceItem();
                    item.ID = reader.GetInt32("ID");
                    item.Title = reader.GetString("Title");
                    item.Quantity = reader.GetDecimal("Quantity");
                    item.Price = reader.GetDecimal("Price");

                    items.Add(item);
                }

                reader.Close();
                connection.Close();
                connection.Dispose();
            }
            catch (Exception ex)
            {

            }
            return items;
        }
        public Invoice GetInvoiceByNumber(int invoice_number)
        {
            Invoice invoice = new Invoice();
            try
            {
                SqlConnection connection = new SqlConnection(_connectionString);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM Invoices WHERE InvoiceNumber = @inv_num";
                cmd.Parameters.AddWithValue("@inv_num", invoice_number);

                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    invoice.InvoiceNumber = (int)reader["InvoiceNumber"];
                    invoice.DateOfIssue = (DateTime)reader["DateOfIssue"];
                    invoice.InvoiceItems = GetInvoiceItemsByInvoiceNumber(invoice.InvoiceNumber);
                }

                reader.Close();
                connection.Close();
                connection.Dispose();
            }
            catch (Exception ex)
            {

            }
            return invoice;
        }
        public object CreateNewInvoice(Invoice new_invoice)
        {
            try
            {
                using(SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO Invoices (DateOfIssue) output INSERTED.InvoiceNumber VALUES (@DateOfIssue)";
                    cmd.Parameters.AddWithValue("@DateOfIssue", new_invoice.DateOfIssue);

                    connection.Open();

                    return cmd.ExecuteScalar();
                }
            }
            catch(Exception ex)
            {

            }

            return null;
        }
        public object CreateNewInvoiceItem(InvoiceItem new_invoiceItem, int invoice_num)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO InvoiceItem (InvoiceNumber, Title, Quantity, Price) VALUES(@InvoiceNumber, @Title, @Quantity, @Price)";
                    cmd.Parameters.AddWithValue("@InvoiceNumber", invoice_num);
                    cmd.Parameters.AddWithValue("@Title", new_invoiceItem.Title);
                    cmd.Parameters.AddWithValue("@Quantity", new_invoiceItem.Quantity);
                    cmd.Parameters.AddWithValue("@Price", new_invoiceItem.Price);

                    connection.Open();
                    int created_invoice_item = cmd.ExecuteNonQuery();
                    return created_invoice_item;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        public void DeleteInvoice(int invoice_number)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM InvoiceItem WHERE InvoiceNumber=@InvoiceNumber";
                    cmd.Parameters.AddWithValue("@InvoiceNumber", invoice_number);

                    SqlCommand cmd2 = new SqlCommand("DELETE FROM Invoices WHERE InvoiceNumber=@InvoiceNumber",connection);
                    cmd2.Parameters.AddWithValue("@InvoiceNumber", invoice_number);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

            }

        }
    }
}
