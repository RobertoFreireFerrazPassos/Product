using Microsoft.AspNetCore.Mvc;
using Product.Models;
using System.Diagnostics;
using System.Threading;
using TaskRunner;

namespace Product.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var form = new FormViewModel()
            {
                Fields = new List<Field>()
                {
                    new TextField("Insert your text:")
                }
            };

            return View(form);
        }

        [HttpPost]
        public async Task<IActionResult> SendFormData([FromBody] FormValuesViewModel form)
        {
            var jsRunnerParams = new JsRunnerParams
            {
                JavascriptCode = EncapsulteJavascriptCodeInModule(form.Code),
                JavascriptCodeIdentifier = "jsTestCode" + DateTime.Now.ToString(),
                Args = new object[] {
                        new {
                            y = form.Values
                        }
                }
            };

            var jsRunner = new JsRunner(10);

            var result = await jsRunner.RunAsync(jsRunnerParams);

            return Ok(result);
        }

        private string EncapsulteJavascriptCodeInModule(string javascriptCode)
        {
            return @"
                module.exports = (callback, input) => {
                    var output = {};
                    "
                    + javascriptCode +
                    @"
                    callback(null, output);
                }";
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}