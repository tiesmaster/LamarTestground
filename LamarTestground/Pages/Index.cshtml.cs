using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LamarTestground.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel(ITestClass testClass)
        {
            TestClass = testClass;
        }

        public ITestClass TestClass { get; }

        public void OnGet()
        {
        }
    }
}
