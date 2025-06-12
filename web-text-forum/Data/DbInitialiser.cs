namespace web_text_forum.Data
{
    public class DbInitialiser
    {
        public static void Initialize(ForumContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var users = new Models.User[]
            {
                new Models.User { Username = "aModerator", Email = "amoderator@host.local", PasswordHash = "hash1", Role = Models.UserRole.Moderator, CreatedAt = DateTime.Now }
            };

            foreach (var user in users)
            {
                context.Users.Add(user);
            }

            context.SaveChanges();
        }
    }
}
