using ResultPattern.Exceptions;
using ResultPatternSample;
using ResultPatternSample.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler<GlobalErrorHandeling>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapGet("/api/method1", () =>
{
    //call service
    var result = Result<string>.Success("Hello World!");

    return Results.Ok(result);
});

app.MapGet("/api/method2", () =>
{
    //call service
    var result = Result<Guid>.Success(Guid.NewGuid());

    return Results.Ok(result);
});

app.MapGet("/api/method3", () =>
{
    //call service
    var result = Result<long>.Success(2500);

    return Results.Ok(result);
});

app.MapGet("/api/method4", () =>
{
    //call service
    var result = BaseResult.Success();

    return Results.Ok(result);
});

app.MapGet("/api/method5", () =>
{
    throw new NotImplementedException();
});

app.MapGet("/api/method6", () =>
{
    throw new AppException("visit codecell.ir");
});

app.MapGet("/api/method7", () =>
{
    throw new NotFoundException("request not found!");
});

app.UseHttpsRedirection();

app.UseExceptionHandler(options => { });
app.Run();
