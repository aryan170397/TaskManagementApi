using TaskManagementApi.Models;

namespace TaskManagementApi.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User { Id = 1, Username = "admin", Password = "admin123", Role = UserRole.Admin },
                    new User { Id = 2, Username = "user1", Password = "user123", Role = UserRole.User },
                    new User { Id = 3, Username = "user2", Password = "user123", Role = UserRole.User }
                );
            }

            if (!context.TaskItems.Any())
            {
                context.TaskItems.AddRange(
                    new TaskItem { Id = 1, Title = "Task 1", Description = "First task", AssignedUserId = 2 },
                    new TaskItem { Id = 2, Title = "Task 2", Description = "Second task", AssignedUserId = 3 }
                );
            }

            context.SaveChanges();
        }
    }
}
