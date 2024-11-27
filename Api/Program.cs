using Microsoft.EntityFrameworkCore;
using Api.Models;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Määritetään tietokanta käyttämään SQLite-tietokantaa
builder.Services.AddDbContext<DataContext>(opt =>
    opt.UseSqlite("Data Source=../Backend/backend.db"),
    ServiceLifetime.Scoped); // Määritetään elinkaari "Scoped"

// Lisää logger-palvelu
builder.Services.AddLogging();

// Lisää Authorization-palvelu
builder.Services.AddAuthorization();

// Lisää Controllers-palvelu
builder.Services.AddControllers();

// Swagger ja dokumentointi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "User API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "User API v1");
        c.RoutePrefix = string.Empty; // Aseta Swagger UI juuripolkuun
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// API-päätepisteet käyttäjien hallintaan
app.MapPost("/users", async (User newUser, DataContext db) =>
{
    db.User.Add(newUser);
    await db.SaveChangesAsync();
    return Results.Created($"/users/{newUser.Id}", newUser);
});

app.MapGet("/users", async (DataContext db) =>
    await db.User.ToListAsync());

app.MapGet("/users/{id}", async (int id, DataContext db) =>
    await db.User.FindAsync(id)
        is User user
            ? Results.Ok(user)
            : Results.NotFound());

app.MapPut("/users/{id}", async (int id, User inputUser, DataContext db) =>
{
    var user = await db.User.FindAsync(id);

    if (user is null) return Results.NotFound();

    user.FirstName = inputUser.FirstName;
    user.LastName = inputUser.LastName;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/users/{id}", async (int id, DataContext db) =>
{
    if (await db.User.FindAsync(id) is User user)
    {
        db.User.Remove(user);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

// API-päätepisteet käyttäjätietojen hallintaan
app.MapGet("/userdata", async (DataContext db) =>
    await db.UserData.ToListAsync());

app.MapGet("/userdata/{id}", async (int id, DataContext db) =>
    await db.UserData.FindAsync(id)
        is UserData userData
            ? Results.Ok(userData)
            : Results.NotFound());

app.MapPost("/userdata", async (UserData userData, DataContext db) =>
{
    db.UserData.Add(userData);
    await db.SaveChangesAsync();

    return Results.Created($"/userdata/{userData.Id}", userData);
});

app.MapPut("/userdata/{id}", async (int id, UserData inputUserData, DataContext db) =>
{
    var userData = await db.UserData.FindAsync(id);

    if (userData is null) return Results.NotFound();

    userData.UserId = inputUserData.UserId;
    userData.Date = inputUserData.Date;
    userData.Value = inputUserData.Value;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/userdata/{id}", async (int id, DataContext db) =>
{
    if (await db.UserData.FindAsync(id) is UserData userData)
    {
        db.UserData.Remove(userData);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

app.Run();