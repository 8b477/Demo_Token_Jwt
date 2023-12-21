
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//********* AJOUT DE MES CLASS DE CONFIGURATION ***************************
AuthenticationService.ConfigureAuthentication(builder.Services);
AuthorizationService.ConfigureAuthorization(builder.Services);
//*************************************************************************


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



//******  ADD   ******* 
app.UseAuthentication();
app.UseAuthorization();
//*********************


app.MapControllers();
app.Run();