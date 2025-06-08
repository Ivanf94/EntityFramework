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
                    inv.InvoiceItems = GetInvoiceItemsByIncoiceNumber(inv.InvoiceNumber);

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

        public List<InvoiceItem> GetInvoiceItemsByIncoiceNumber(int invoiceNumber)
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
                    invoice.InvoiceItems = GetInvoiceItemsByIncoiceNumber(invoice.InvoiceNumber);
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
            return null;
        }
        public object CreateNewInvoiceItem(InvoiceItem new_invoiceItem, int invoice_num)
        {
            return null;
        }
        public void DeleteInvoice(int invoice_number)
        {

        }
    }
}
