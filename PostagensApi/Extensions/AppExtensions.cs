﻿namespace PostagensApi.Extensions
{
    public static class AppExtensions
    {
        public static void ConfigureDevEnvironment(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            //app.MapSwagger().RequireAuthorization();
        }
    }
}
