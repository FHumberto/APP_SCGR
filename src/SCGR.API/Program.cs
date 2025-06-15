using SCGR.API;
using SCGR.Application;
using SCGR.Infrastructure;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

builder.Services.AddPresentationLayer();
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer();

WebApplication? app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
