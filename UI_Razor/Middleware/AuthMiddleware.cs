namespace UI_Razor.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Bỏ qua kiểm tra nếu đang ở trang Login của MVC (tránh vòng lặp redirect)
            if (context.Request.Path.StartsWithSegments("/Users/Login"))
            {
                await _next(context);
                return;
            }

            // Kiểm tra xem User đã đăng nhập chưa
            var userRole = context.Request.Cookies["UserRole"];
            var userId = context.Request.Cookies["UserID"];

            // Nếu chưa đăng nhập hoặc không phải Admin → Chuyển về trang Login của MVC
            if (userId == null || string.IsNullOrEmpty(userRole) || userRole != "Admin")
            {
                context.Response.Redirect("https://localhost:7266/"); // Chuyển về trang Login của MVC
                return;
            }

            await _next(context); // Nếu là Admin, cho phép truy cập
        }

    }
}
