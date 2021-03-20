using System.Collections;
using System.Collections.Generic;

namespace Domain
{
    public class TodoTaskCategory
    {
        public int TodoTaskCategoryId { get; set; }
        public string TodoTaskCategoryName { get; set; } = default!;
        public ICollection<TodoTask>? TodoTasks { get; set; } = default!;
    }
}