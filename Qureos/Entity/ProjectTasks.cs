using System.ComponentModel.DataAnnotations;

namespace Qureos.Entity
{
    public class ProjectTasks
    {
        [Key]
        public int TaskId { get; set; }

        public string TaskName { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int AssignedTo { get; set; }

        public int Priority { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }

       public ICollection<TasksComment> Comments { get; set; }
    }
}
