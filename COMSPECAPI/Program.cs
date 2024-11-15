
using Amazon;
using Amazon.Runtime;
using Amazon.S3;

namespace COMSPECAPI
{
    public class Program
    {
        public static void Main(string[] args) 
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var awsSettings = configuration.GetSection("AWS");
            var credential = new BasicAWSCredentials(awsSettings[
                "AccessKeyId"], awsSettings["SecretAccessKey"]);

            var awsOptions = configuration.GetAWSOptions();
            awsOptions.Credentials = credential;
            awsOptions.Region = RegionEndpoint.USEast1;
            builder.Services.AddDefaultAWSOptions(awsOptions);
           
            
            builder.Services.AddAWSService<IAmazonS3>();




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
