using Asas.AsasHash.Contracts;
using Asas.AsasHash.Models;
using Asas.Data;
using Asas.Models;
using Azure.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace AsasAPIs.EndpointRoute
{
    public static class Login
    {

         public record LoginRequest(string Email, string Pass);
        public static IEndpointRouteBuilder MapLoginEndpoints(this IEndpointRouteBuilder builder)
        {
            var loginGroup = builder.MapGroup("/api/Login");

            loginGroup.MapPost("/", async (
                LoginRequest request,
                AsasContext dbContext,
                IAsasHashPassword hashService
                )// lambda parameters
              =>
            {
                var compAcc = await dbContext.Acc.FirstOrDefaultAsync(a => a.Email == request.Email);
                if (compAcc == null)
                {
                    return Results.BadRequest(new { Message = "البريد الإلكتروني أو كلمة المرور غير صحيحة" });
                }
                var loginContext = new Hash
                {
                    RawPassword = request.Pass,
                    HashedPassword = compAcc.Pass,
                    GoodHash = new GoodHash
                    {
                        IV = compAcc.IV,
                        Iterations = compAcc.Iterations,
                        Salt = compAcc.Salt
                    }
                };

                var verifyResult = hashService.VerifyPassword(loginContext);
                if (!verifyResult.IsSucceeded)
                {
                    return Results.BadRequest(new { Message = "البريد الإلكتروني أو كلمة المرور غير صحيحة" });
                }
                return Results.Ok(new { Message = "تم تسجيل الدخول بنجاح" });

            }// lambda

            );//MapPost

               return builder;

        }// IEndpointRouteBuilder




    }
    
}
