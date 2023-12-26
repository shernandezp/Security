using Common.Infrastructure;

namespace Security.Infrastructure.Entities;

public sealed class User(string username,
    string password,
    string email,
    string firstName,
    string? secondName,
    string lastName,
    string? seconSurname,
    DateTime? dOB,
    Guid accountId) : BaseAuditableEntity
{
    public Guid UserId { get; private set; } = Guid.NewGuid();
    public string Username { get; set; } = username;
    public string Password { get; set; } = password;
    public string Email { get; set; } = email;
    public string FirstName { get; set; } = firstName;
    public string? SecondName { get; set; } = secondName;
    public string LastName { get; set; } = lastName;
    public string? SeconSurname { get; set; } = seconSurname;
    public DateTime? DOB { get; set; } = dOB;
    public DateTime? PasswordReset { get; set; }
    public DateTime? Verified { get; set; }
    public bool Active { get; set; } = false;
    public Guid AccountId { get; set; } = accountId;
    public Account? Account { get; set; }
    public IEnumerable<Role> Roles { get; } = new HashSet<Role>();
    public IEnumerable<Profile> Profiles { get; } = new HashSet<Profile>();
}
