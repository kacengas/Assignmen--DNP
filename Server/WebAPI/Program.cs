using FileRepositories;
using MiddleWare;
using RepositoryContracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPostRepository , PostFileRepository>();
builder.Services.AddScoped<IUserRepository, UserFileRepository>();
builder.Services.AddScoped<ICommentRepository, CommentFileRepository>();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

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