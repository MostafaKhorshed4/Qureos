namespace Qureos.Entity
{
    public class TasksComment
    {
        
        public int UserId { get; set; }
        public User user { get; set; }
        public int TaskId { get; set; }
        public ProjectTasks task { get; set; }

        public DateTime CommentDate { get; set; }
        public string Comment { get; set; }

    }
}
