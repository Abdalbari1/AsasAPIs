using AsasAPIs.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AsasContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AsasContext")
    ?? throw new InvalidOperationException("Connection string 'AsasContext' not found.")));
// إضافة خدمات المصادقة إلى حاوية الخدمات
builder.Services.AddAuthentication(options => 
{
    // JWT مخطط التحقق الافتراضي للـ 
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    // مخطط التحدي الافتراضي عند رفض الدخول
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    // المخطط العام الأساسي للمصادقة
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;           
    // إضافة ودعم توكن JWT وتخصيص خيارات
}).AddJwtBearer(options => 
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters // إعداد معايير التحقق
    {
        // التحقق من الجهة المُصدرة للتوكن
        ValidateIssuer = true,
        // التحقق من الجهة المستهدفة (التطبيق الحالي)
        ValidateAudience = true,
        // التحقق من وقت الصلاحية (ألا يكون منتهياً)
        ValidateLifetime = true,
        // التحقق من مفتاح التوقيع لضمان عدم التلاعب بالبيانات
        ValidateIssuerSigningKey = true,
        // جلب الجهة المُصدرة المعتمدة من ملف الإعدادات
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        // جلب الجهة المستهدفة المعتمدة من ملف الإعدادات
        ValidAudience = builder.Configuration["Jwt:Audience"],
        // بناء مفتاح التوقيع الرقمي
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey( 
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT key not found.")) // تحويل النص السري إلى بايتات
        )
    };
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddCors(options => {
    options.AddPolicy("_myAllowSpecificOrigins", p => {
        p.AllowAnyOrigin()
         .AllowAnyMethod()
         .AllowAnyHeader();
    });
});
// init Hash password 
builder.Services.AddScoped<Asas.AsasHash.Contracts.IAsasHashPassword, Asas.AsasHash.AsasHash>();
// init JWT service  
builder.Services.AddScoped<AsasAPIs.Services.JWT.JwtService>();

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Asas APIs v1");
        options.RoutePrefix = "swagger";
    });
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseCors("_myAllowSpecificOrigins");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();