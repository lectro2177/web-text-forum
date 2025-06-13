using Microsoft.AspNetCore.Authorization;

namespace web_text_forum.Attributes
{
    public class BasicAuthorizeAttribute : AuthorizeAttribute
    {
        public BasicAuthorizeAttribute()
        {
            Policy = "BasicAuthentication";
        }
    }
}