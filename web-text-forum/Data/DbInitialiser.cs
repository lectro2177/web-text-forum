using Microsoft.VisualBasic;
using web_text_forum.Security;

namespace web_text_forum.Data
{
    public class DbInitialiser
    {
        public static void Initialize(ForumContext context)
        {
            context.Database.EnsureCreated();

            // Look for any users.
            if (context.Users.Any())
            {
                return;   // DB has been seeded (yes this is just a basic check - TODO: perhaps improve later)
            }

            using var transaction = context.Database.BeginTransaction();
            
            try
            {
                // Create one moderator and three regular users.
                var users = new Models.User[]
                {
                    new Models.User { Username = "moderator1", Email = "moderator1@host.local", PasswordHash = PasswordHasher.GetBase64Hash_SHA256("SuperSecure1024!"), Role = Models.UserRole.Moderator, CreatedAt = DateTime.Now },
                    
                    new Models.User { Username = "user1", Email = "user1@host.local", PasswordHash = PasswordHasher.GetBase64Hash_SHA256("MyPetsName2025"), Role = Models.UserRole.RegualarUser, CreatedAt = DateTime.Now },
                    new Models.User { Username = "user2", Email = "user2@host.local", PasswordHash = PasswordHasher.GetBase64Hash_SHA256("MyBirthday1980"), Role = Models.UserRole.RegualarUser, CreatedAt = DateTime.Now },
                    new Models.User { Username = "user3", Email = "user3@host.local", PasswordHash = PasswordHasher.GetBase64Hash_SHA256("TooShort1234"), Role = Models.UserRole.RegualarUser, CreatedAt = DateTime.Now }
                };

                foreach (var user in users)
                {
                    context.Users.Add(user);
                }
                context.SaveChanges();

                // Create a tag with the description 'misleading or false information'
                var tag = new Models.Tag
                {
                    TagDescription = "Misleading or false information."
                };

                context.Tags.Add(tag);
                context.SaveChanges();

                // Create some posts by the various users (the last user's post will be tagged as 'misleading or false information').
                var posts = new List<Models.Post>();

                int userNumber = 0;
                int userCount = users.Count();

                foreach (var user in users)
                {
                    userNumber++;

                    Models.Post post = new()
                    {
                        Content = $"This is the content of a post by {user.Username}.",
                        UserId = user.Id,
                        CreatedAt = DateTime.Now
                    };

                    // Only the last user will have a post tagged as 'misleading or false information'.
                    if (userNumber == userCount)
                        post.TagId = tag.Id;

                    posts.Add(post);

                    context.Posts.Add(post);
                }
                context.SaveChanges();

                // Create some comments on all the posts.
                var comments = new List<Models.Comment>();

                foreach (var post in posts)
                {
                    foreach (var user in users)
                    {
                        if (user.Id == post.UserId) continue; // Skip the post author

                        var comment = new Models.Comment
                        {
                            PostId = post.Id,
                            UserId = user.Id,
                            Content = $"This is comment by {user.Username} on post {post.Id}.",
                            CreatedAt = DateTime.Now
                        };

                        comments.Add(comment);

                        context.Comments.Add(comment);
                    }
                }
                context.SaveChanges();

                // Create some likes on all the posts (excluding by each post's author).
                var likes = new List<Models.Like>();

                foreach (var post in posts)
                {
                    foreach (var user in users)
                    {
                        if (user.Id == post.UserId) continue; // Skip the post author

                        var like = new Models.Like
                        {
                            PostId = post.Id,
                            UserId = user.Id,
                            CreatedAt = DateTime.Now
                        };

                        likes.Add(like);

                        context.Likes.Add(like);
                    }
                }
                context.SaveChanges();

                // Commit the transaction
                transaction.Commit();
            }
            catch (Exception ex)
            {
                // TODO: Log the exception ...

                // Rollback the transaction in case of error                
                transaction.Rollback();
                
                throw;
            }
        }
    }
}
