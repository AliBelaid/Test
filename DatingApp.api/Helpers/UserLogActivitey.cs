using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using DatingApp.api.Data;
using System;

namespace DatingApp.api.Helpers
{
    public class UserLogActivitey : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
             var contentReult = await next();
        var userId = int.Parse(contentReult.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var repo = contentReult.HttpContext.RequestServices.GetService<IUserRepository>();
        var user =await repo.GetUser(userId);
        user.LastActive = DateTime.Now ;
       await repo.SaveAll(); 
        }

}}