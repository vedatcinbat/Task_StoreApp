using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using StoreApp.API.Data;
using StoreApp.API.Data.Mappings;
using StoreApp.API.Validators;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson().AddFluentValidation(fv =>
{
    fv.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>();
    fv.RegisterValidatorsFromAssemblyContaining<CreateCategoryValidator>();
    fv.RegisterValidatorsFromAssemblyContaining<ProductFilterSortPaginationValidator>();
    fv.RegisterValidatorsFromAssemblyContaining<UpdateProductValidator>();
    fv.RegisterValidatorsFromAssemblyContaining<JsonPatchValidator>();
    fv.RegisterValidatorsFromAssemblyContaining<CategoryFilterSortPaginationValidator>();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext Injection
builder.Services.AddDbContext<StoreAppDbContext>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("storeappConnectionString"),
        new MySqlServerVersion(new Version(8, 0, 21)));
});



// AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
