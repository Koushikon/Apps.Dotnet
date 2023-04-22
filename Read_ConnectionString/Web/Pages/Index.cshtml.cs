using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _enviroment;

        public string ConnectionString { get; set; } = string.Empty;
        public string WebRootPath { get; set; } = string.Empty;

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration, IWebHostEnvironment environment)
        {
            _logger = logger;
            _configuration = configuration;
            _enviroment = environment;
        }

        public void OnGet()
        {
            ConnectionString = _configuration.GetValue<string>("ModulesTwo:Users:Database")!;
            WebRootPath = _enviroment.WebRootPath!;
        }
    }
}