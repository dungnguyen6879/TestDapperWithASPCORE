//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace WebAPI
{
    public class ApplicationDBContext 
    {
        private readonly IConfiguration _configuration;

        public ApplicationDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
            
        }
        
        public IDbConnection CreateConnection()
        => new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
    }
}
