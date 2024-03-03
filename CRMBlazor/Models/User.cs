using CRMBlazor.Interface;

namespace CRMBlazor.Models
{
    public class User : IName, ITestik
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int UserRoleId { get; set; }
        public Role UserRole { get; set; }
        public List<UserTask> UserTasks { get; set; }

    }
}
