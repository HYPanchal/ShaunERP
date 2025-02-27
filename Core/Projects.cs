#nullable enable

using System;
using System.Collections.Generic;

namespace Core
{
    // Represents a project which contains issues and sprints.
    public class Project : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        // Collection of issues (tickets) associated with this project.
        public ICollection<Issue> Issues { get; set; } = new List<Issue>();

        // Collection of sprints (iterations) for agile project management.
        public ICollection<Sprint> Sprints { get; set; } = new List<Sprint>();

        // Optionally, add members or other project-specific properties here.
    }

    // Represents an issue (or ticket) within a project.
    public class Issue : BaseEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public IssueType Type { get; set; }
        public IssueStatus Status { get; set; }
        public IssuePriority Priority { get; set; }
        public DateTime? DueDate { get; set; }

        // Foreign key reference to the project.
        public int ProjectId { get; set; }
        public Project? Project { get; set; }

        // Collection of comments attached to the issue.
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        // Collection of work logs (time tracking) for this issue.
        public ICollection<WorkLog> WorkLogs { get; set; } = new List<WorkLog>();
    }

    // Enumeration for types of issues.
    public enum IssueType
    {
        Bug,
        Feature,
        Task,
        Improvement
    }

    // Enumeration for the status of an issue.
    public enum IssueStatus
    {
        New,
        Open,
        InProgress,
        Resolved,
        Closed
    }

    // Enumeration for issue priority levels.
    public enum IssuePriority
    {
        Low,
        Medium,
        High,
        Critical
    }

    // Represents a comment on an issue.
    public class Comment : BaseEntity
    {
        public string? Text { get; set; }
        public DateTime CommentDate { get; set; } = DateTime.UtcNow;

        // Optionally, reference a user (or just store a name) who made the comment.
        public string? Author { get; set; }

        // Foreign key reference to the associated issue.
        public int IssueId { get; set; }
        public Issue? Issue { get; set; }
    }

    // Represents a sprint (iteration) in agile project management.
    public class Sprint : BaseEntity
    {
        public string? Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Foreign key reference to the associated project.
        public int ProjectId { get; set; }
        public Project? Project { get; set; }

        // Issues planned for or assigned to this sprint.
        public ICollection<Issue> Issues { get; set; } = new List<Issue>();
    }

    // Represents a work log entry for tracking time spent on an issue.
    public class WorkLog : BaseEntity
    {
        // Foreign key reference to the associated issue.
        public int IssueId { get; set; }
        public Issue? Issue { get; set; }

        // Number of hours logged (or use TimeSpan if preferred).
        public double HoursSpent { get; set; }
        public string? Description { get; set; }
        public DateTime LogDate { get; set; } = DateTime.UtcNow;

        // Optionally, store who logged the work.
        public string? LoggedBy { get; set; }
    }
}
