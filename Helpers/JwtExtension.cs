namespace CityGuide.Helpers
{
    public static class JwtExtension
    {
        public static void AddApplicationError(this HttpResponseMessage response, string message) {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Headers.Add("Access-Control-Expose-Header", "Application-Error");
        }
    }
}
