
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers()
.AddJsonOptions(opt =>
                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PublisherData.PubContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("PubConnection"))
    .EnableSensitiveDataLogging()
    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
