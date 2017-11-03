using System;

namespace Core.Models
{
    // An interface to support data accessibility
    public interface IUserModel : IPoco
    {
        // Primary key, read only  
        Guid Id { get; }
        // Created date, read only
        DateTime Created { get; }
        // Modified date, read only
        DateTime Modified { get; }
        // User first name
        string FirstName { get; set; }
        // User last name
        string LastName { get; set; }
        // User email
        string Email { get; set; }
    }

    // POCO object to support data persistence
    public class UserModel : Poco, IUserModel
    {
        // Primary key 
        public Guid Id { get; set; }
        // Created date
        public DateTime Created { get; set; }
        // Modified date
        public DateTime Modified { get; set; }
        // User first name
        [PropertyChanged]
        public string FirstName { get; set; }
        // User last name
        [PropertyChanged]
        public string LastName { get; set; }
        // User email
        [PropertyChanged]
        public string Email { get; set; }
    }    
}
