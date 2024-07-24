using Microsoft.AspNetCore.Builder;

namespace TestStaticFile
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = async (ctx) =>
               {
                   Console.WriteLine(ctx.File.Name);
                   if (ctx.File.Name.EndsWith(".html") || ctx.File.Name.EndsWith(".htm"))
                   {
                       HttpResponse response = ctx.Context.Response;
                       var responseOriginalStream = response.Body;
                       try
                       {
                           var asdfasd = "<script src=\"./jquery/dist/jquery.min.js\"></script><script src=\"./globalsetup.js\"></script>";                          
                           await response.Body.WriteAsync(System.Text.UTF8Encoding.UTF8.GetBytes(asdfasd), 0, System.Text.UTF8Encoding.UTF8.GetBytes(asdfasd).Length);
                           //using (var memoryResponseStream = new MemoryStream())
                           //{
                           //    StreamReader reader = new StreamReader(response.BodyWriter.AsStream());
                           //    string textBody = reader.ReadToEnd();
                           //    textBody.Replace("<body>", "<body> ");
                           //    StreamWriter writer = new StreamWriter(memoryResponseStream);
                           //    writer.Write(textBody);
                           //    writer.Flush();
                           //    //更换默认的Response流对象
                           //    response.Body = memoryResponseStream;
                           //    memoryResponseStream.Seek(0, SeekOrigin.Begin);
                           //    await memoryResponseStream.CopyToAsync(responseOriginalStream);
                           //}
                       }
                       catch (Exception e)
                       {
                           await Console.Out.WriteLineAsync(e.Message);
                       }
                       finally
                       {
                         //  response.Body = responseOriginalStream;
                       }
                   }
               }
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
