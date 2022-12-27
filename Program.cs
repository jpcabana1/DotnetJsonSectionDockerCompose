var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.MapGet("/env-var", (IConfiguration configuration) =>
    configuration.GetSection("Core").Get<Core>()
).WithName("GetEnvVar");

app.Run();


record SectionConfiguration
{
    public Core? Core { get; set; }
}

record Core
{
    public Application? Application { get; set; }
    public Storage? Storage { get; set; }
}

record Application
{
    public string? UrlHost { get; set; }
}

record Storage
{
    public string? ConnectionString { get; set; }
    public string? ContainerName { get; set; }
}
