using ChatNow_WebAPi.Hubs;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials();
        });
});
builder.Services.AddControllers();

// Adiciona servi�o de autentica��o JWT Bearer
builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = "JwtBearer";
    options.DefaultAuthenticateScheme = "JwtBearer";
})
.AddJwtBearer("JwtBearer", options => // Define os par�metros de valida��o do token
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        // Valida quem est� solicitando
        ValidateIssuer = true,

        // Valida quem est� recebendo
        ValidateAudience = true,

        // Define se o tempo de exibi��o do token ser� validado
        ValidateLifetime = true,

        // Forma de criptografia e ainda valida��o da chave de autentica��o
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("chatnow-authentication-key-j321u432")),

        // Valida o tempo de expira��o do token
        ClockSkew = TimeSpan.FromMinutes(5),

        // De onde est� vindo (issuer)
        ValidIssuer = "webapi.chatnow",

        // Para onde est� indo (audience)
        ValidAudience = "webapi.chatnow",
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API ChatNow",
        Description = "API para projeto ChatNow - API .NET com integra��o a projeto React JS",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Lucas Bianchezzi Oliveira",
            Url = new Uri("https://github.com/LucasBO7")
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"; options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    // Usando a autentica��o do Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Value: Bearer TokenJWT"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = "swagger";
    });
}

app.UseCors(options =>
        options.WithOrigins("http://localhost:5173")
        .AllowAnyHeader()
        .WithMethods("GET", "POST")
        .AllowCredentials()
);
app.UseHttpsRedirection();

// Usar autentica��o
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/chat");
app.UseCors("AllowOrigin");

app.Run();