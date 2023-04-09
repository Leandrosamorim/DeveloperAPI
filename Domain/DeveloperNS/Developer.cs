using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.ContactNS;
using Domain.ProgrammingStackNS;



namespace Domain.DeveloperNS
{
    public class Developer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UId { get; set; }
        public string Name { get; set; }
        [ForeignKey("ContactId")]
        public Contact Contact { get; set; }
        public int StackId { get; set; }
        [ForeignKey("StackId")]
        public ProgrammingStack Stack { get; set; }
        public string Login { get;set; }
        public string Password { get; set; }
    }

    public static class DeveloperFactory
    {
        public static Developer CreateDeveloper(string name, Contact contact, int stackId, string login, string password)
        {
            return new Developer
            {
                Name = name,
                Contact = contact,
                StackId = stackId,
                Login = login,
                Password = BCrypt.Net.BCrypt.HashPassword(password)
            };
        }
    }
}
