using Dapper;
using Humanizer;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Data;
using System.Text;
using WebAPI.Models;
using WebAPI.Models.DTO;
using static Dapper.SqlMapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.Mime.MediaTypeNames;

namespace WebAPI.Services
{
    public class ContactRepository : IContact
    {
        private readonly ApplicationDBContext _context;
        public ContactRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Contact>> GetContacts()
        {
            var query = "SELECT * FROM Contact";
            using (var connection = _context.CreateConnection())
            {
                var contacts = await connection.QueryAsync<Contact>(query);
                return contacts.ToList();
            }
        }

        public async Task<Contact> GetContact(Guid id)
        {
            var query = "SELECT * FROM Contact WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                var contact = await connection.QuerySingleOrDefaultAsync<Contact>(query, new { id });
                return contact;
            }
        }
        public async Task<Contact> CreateContact(ContactDTO contact)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" INSERT INTO Contact");
            sql.Append(" (");
            sql.Append("     Id");
            sql.Append("    ,Title");
            sql.Append("    ,FirstName");
            sql.Append("    ,LastName");
            sql.Append("    ,Email");
            sql.Append("    ,WebsiteUrl");
            sql.Append("    ,PhoneNumber");
            sql.Append("    ,IsDeleted");
            sql.Append("    ,CreatedOn");
            sql.Append("    ,UpdatedOn");
            sql.Append("    ,CreatedBy");
            sql.Append("    ,UpdatedBy");
            sql.Append(" )");
            sql.Append(" VALUES");
            sql.Append(" (");
            sql.Append("     @Id");
            sql.Append("    ,@Title");
            sql.Append("    ,@FirstName");
            sql.Append("    ,@LastName");
            sql.Append("    ,@Email");
            sql.Append("    ,@WebsiteUrl");
            sql.Append("    ,@PhoneNumber");
            sql.Append("    ,@IsDeleted");
            sql.Append("    ,@CreatedOn");
            sql.Append("    ,@UpdatedOn");
            sql.Append("    ,@CreatedBy");
            sql.Append("    ,@UpdatedBy");
            sql.Append(" )");
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Id", contact.Id, DbType.Guid);
                    parameters.Add("@Title", contact.Title, DbType.String);
                    parameters.Add("@FirstName", contact.FirstName, DbType.String);
                    parameters.Add("@LastName", contact.LastName, DbType.String);
                    parameters.Add("@Email", contact.Email, DbType.String);
                    parameters.Add("@WebsiteUrl", contact.WebsiteUrl, DbType.String);
                    parameters.Add("@PhoneNumber", contact.PhoneNumber, DbType.String);
                    parameters.Add("@IsDeleted", contact.IsDeleted, DbType.Boolean);
                    parameters.Add("@CreatedBy", contact.Id, DbType.Guid);
                    parameters.Add("@CreatedOn", contact.CreatedOn, DbType.DateTime);
                    parameters.Add("@UpdatedBy", contact.UpdatedBy, DbType.Guid);
                    parameters.Add("@UpdatedOn", contact.UpdatedOn, DbType.DateTime);
                    // Todo: save database
                    await connection.ExecuteAsync(sql.ToString(), parameters);
                    //Todo: show result 
                    var createdContact = new Contact
                    {   
                        Id = contact.Id,
                        Title = contact.Title,
                        FirstName = contact.FirstName,
                        LastName = contact.LastName,
                        Email = contact.Email,
                        WebsiteUrl = contact.WebsiteUrl,
                        PhoneNumber = contact.PhoneNumber,
                        IsDeleted = contact.IsDeleted,
                        CreatedBy = contact.Id,
                        CreatedOn = contact.CreatedOn
                    };

                    return createdContact;
                }
            } catch (Exception)
            {
                throw;
            }
            
        }
        public async Task UpdateContact(Guid id, ContactDTO contact)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" UPDATE Contact");
            sql.Append(" SET");
            sql.Append("     Title = @Title");
            sql.Append("    ,FirstName = @FirstName");
            sql.Append("    ,LastName = @LastName");
            sql.Append("    ,Email = @Email");
            sql.Append("    ,WebsiteUrl = @WebsiteUrl");
            sql.Append("    ,PhoneNumber = @PhoneNumber");
            sql.Append("    ,IsDeleted = @IsDeleted");
            sql.Append("    ,UpdatedOn = @UpdatedOn");
            sql.Append("    ,UpdatedBy = @UpdatedBy");
            sql.Append(" WHERE");
            sql.Append("    Id = @Id");
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Id", id, DbType.Guid);
                    parameters.Add("@Title", contact.Title, DbType.String);
                    parameters.Add("@FirstName", contact.FirstName, DbType.String);
                    parameters.Add("@LastName", contact.LastName, DbType.String);
                    parameters.Add("@Email", contact.Email, DbType.String);
                    parameters.Add("@WebsiteUrl", contact.WebsiteUrl, DbType.String);
                    parameters.Add("@PhoneNumber", contact.PhoneNumber, DbType.String);
                    parameters.Add("@IsDeleted", contact.IsDeleted, DbType.Boolean);
                    parameters.Add("@UpdatedBy", id, DbType.Guid);
                    parameters.Add("@UpdatedOn", contact.UpdatedOn, DbType.DateTime);

                    await connection.ExecuteAsync(sql.ToString(), parameters);
                }
            }catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteContact(Guid id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" DELETE FROM Contact");
            sql.Append(" WHERE ");
            sql.Append("     Id = @Id");

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(sql.ToString(), new { id });
                }
            }catch(Exception)
            {
                throw;
            }
            
        }
        public async Task<Int16> AddContactByProcedure(ContactDTO contact)
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    //Todo: Call store procedure
                    var procedureName = "Add_Contact";
                    var parameters = new DynamicParameters();
                    parameters.Add("@Title", contact.Title, DbType.String, ParameterDirection.Input);
                    parameters.Add("@FirstName", contact.FirstName, DbType.String, ParameterDirection.Input);
                    parameters.Add("@LastName", contact.LastName, DbType.String, ParameterDirection.Input);
                    parameters.Add("@Email", contact.Email, DbType.String, ParameterDirection.Input);
                    parameters.Add("@WebsiteUrl", contact.WebsiteUrl, DbType.String, ParameterDirection.Input);
                    parameters.Add("@PhoneNumber", contact.PhoneNumber, DbType.String, ParameterDirection.Input);
                    parameters.Add("@IsDeleted", contact.IsDeleted, DbType.Boolean, ParameterDirection.Input);
                    parameters.Add("@Status", 1, DbType.Int16, direction: ParameterDirection.Output);
                    //Todo: Exec Store procedure
                    await connection.ExecuteScalarAsync(procedureName, parameters, commandType: CommandType.StoredProcedure);
                    //Todo: Show Result
                    var status = parameters.Get<Int16>("@Status");
                    return status;
                }
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        public async Task<int> RemoveContactByProcedure(Guid id)
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    //Todo: Call store procedure
                    var procedure = "Remove_Contact";
                    var parameters = new DynamicParameters();
                    parameters.Add("@Id", id, DbType.Guid, direction: ParameterDirection.Input);
                    parameters.Add("@Status", 0, DbType.Int32, direction: ParameterDirection.ReturnValue);
                    //Todo: Exec Store procedure
                    await connection.ExecuteScalarAsync(procedure, parameters, commandType: CommandType.StoredProcedure);
                    //Todo: Show Result
                    var status = parameters.Get<int>("@Status");
                    return status;
                }
            }
            catch (Exception)
            {
                throw;
            };
        }
    }
}
