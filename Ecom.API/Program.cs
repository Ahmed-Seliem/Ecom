using Ecom.API.Middleware;
using Ecom.INFRASTRUCTURE;
using Microsoft.AspNetCore.Cors.Infrastructure;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(
    op =>
    {
        op.AddPolicy("CORSPolicy", builder =>
        {
            builder.AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials()
                   .WithOrigins("http://localhost:4200");
        });
    });

builder.Services.AddMemoryCache();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.infrastructureConfiguration(builder.Configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CORSPolicy");
app.UseMiddleware<ExceptionMiddleware>();
app.UseStaticFiles();
app.UseStatusCodePagesWithReExecute("/errors/{0}");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
