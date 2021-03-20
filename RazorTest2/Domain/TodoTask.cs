using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class TodoTask
    {
        public int TodoTaskId { get; set; }

        [MaxLength(128)]
        [MinLength(5)]
        public string TodoTaskName { get; set; } = default!;
        
        public bool IsDone { get; set; }

        public int TodoTaskPriorityId { get; set; }
        public TodoTaskPriority? TodoTaskPriority { get; set; } = default!;

        public int TodoTaskCategoryId { get; set; }
        public TodoTaskCategory? TodoTaskCategory { get; set; } = default!;
    }
}