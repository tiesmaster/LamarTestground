using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LamarTestground.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel(TestClass testClass)
        {
            TestClass = testClass;
        }

        public TestClass TestClass { get; }

        public void OnGet()
        {
        }
    }
}
