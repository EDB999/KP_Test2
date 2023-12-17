using AngleSharp;
using AngleSharp.Html.Parser;

namespace Service
{
    public class ServiceRoute
    {
        public async Task<String?> GetRoute(string from, string to)
        {
            var config = Configuration.Default.WithDefaultLoader();
            using var context = BrowsingContext.New(config);

            var url = @"https://ru.distance.to/" + from + @"-Воронежская-область/" + to + "-Воронежская-область";

            using var doc = await context.OpenAsync(url);
            var pars = doc.QuerySelectorAll(@"span[class=""value km""]").First().InnerHtml;

            return pars;
        }
    }
}