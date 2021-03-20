using System.Collections;
using System.Collections.Generic;

namespace Domain
{
    public class TodoTaskPriority
    {
        public int TodoTaskPriorityId { get; set; }
        public string TodoTaskPriorityName { get; set; } = default!;
        public int Sort { get; set; }

        public ICollection<TodoTask>? Todotasks { get; set; } = default!;
    }
}