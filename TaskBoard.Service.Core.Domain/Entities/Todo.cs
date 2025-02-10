using System.ComponentModel.DataAnnotations;
using TaskBoard.Framework.Core.Entities;

namespace TaskBoard.Service.Core.Domain.Entities;

public class Todo : BaseOwnedEntity<int, User>
{
    [MaxLength(30)]
    public string Title { get; set; }

    [MaxLength(250)]
    public string Description { get; set; }

    public bool? IsCompleted { get; set; } = null;

    public DateOnly? DueDate { get; set; } = null;

    public override void CopyFrom(BaseOwnedEntity<int, User> other)
    {
        if (other is Todo t)
        {
            if (!string.IsNullOrEmpty(t.Title))
            {
                this.Title = t.Title;
            }
            if (!string.IsNullOrEmpty(t.Description))
            {
                this.Description = t.Description;
            }
            if (t.IsCompleted != null)
            {
                this.IsCompleted = t.IsCompleted;
            }
            if (t.DueDate != null)
            {
                this.DueDate = t.DueDate;
            }
        }
    }
}
