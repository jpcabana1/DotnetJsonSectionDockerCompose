var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.MapGet("/env-var", (IConfiguration configuration) =>
{
    var UrlHost = configuration.GetValue<string>("Application:UrlHost");
    var ConnectionString = configuration.GetValue<string>("Storage:ConnectionString");
    var ContainerName = configuration.GetValue<string>("Storage:ContainerName");
    var ArrayOne = configuration.GetValue<string>("Array:0:Value");
    var ArrayTwo = configuration.GetValue<string>("Array:1:Value");

    dynamic resp = new {
        url = UrlHost,
        conString = ConnectionString,
        name = ContainerName,
        ArrayOne = ArrayOne,
        ArrayTwo = ArrayTwo
    };
    return resp;
}
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
