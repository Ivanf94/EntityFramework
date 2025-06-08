using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ConnectionString_custom_repo.Repository
{
    public class HomeRepository
    {
        private readonly IConfiguration _configuration;

        public HomeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool CheckExistingConnection()
        {
            try
            {
                using(SqlConnection con = new SqlConnection(_configuration.GetConnectionString("Invoices")))
                {
                    con.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool CheckNewConnection()
        {
            try
            {
                using(SqlConnection con = new SqlConnection(_configuration.GetConnectionString("NotAvailable")))
                {
                    con.Open();
                    return true;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
