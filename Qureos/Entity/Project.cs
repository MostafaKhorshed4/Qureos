using System.ComponentModel.DataAnnotations;

namespace Qureos.Entity
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public string Owner { get; set; }
        public double Budget { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ICollection<ProjectTasks> Tasks { get; set; }


    }

    public enum Status
    {
        NotStarted,
        InProgress,
        Completed
    }
}
