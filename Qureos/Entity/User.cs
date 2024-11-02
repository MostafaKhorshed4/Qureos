using System.ComponentModel.DataAnnotations;

namespace Qureos.Entity
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Role role { get; set; }
       public ICollection<TasksComment> Comments { get; set; }

    }
    public enum Role
    {
        manager , 
        Employee
    }
}
