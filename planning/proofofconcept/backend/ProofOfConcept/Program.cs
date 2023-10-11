
using Microsoft.AspNetCore.Builder;
using ProofOfConcept.Hubs;

namespace ProofOfConcept
{
    public class Program
    {
        public static readonly Dictionary<string, Type> HubList = new Dictionary<string, Type>()
        {
            { "/ws", typeof(TestSignalRHub) }
        };

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSignalR();

#if DEBUG
            builder.Services.AddSigSpecDocument(o => o.Hubs["/ws"] = typeof(TestSignalRHub));
#endif

            var app = builder.Build();

#if DEBUG
            // https://localhost:8729/swagger/v1/swagger.json
            app.UseSwagger();
            app.UseSwaggerUI();

            // https://localhost:8729/sigspec/v1/sigspec.json
            app.UseSigSpec();
            app.UseSigSpecUi();
#endif

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
            app.MapHub<TestSignalRHub>(HubList.First(hl => hl.Value == typeof(TestSignalRHub)).Key);

            app.Run();
        }
    }
}