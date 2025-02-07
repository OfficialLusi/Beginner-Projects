using Microsoft.AspNetCore.Mvc;

namespace UnitConverter.Controllers
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
            return View();
        }

        public IActionResult Length()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Length(double value, string unitFrom, string unitTo)
        {
            double result = ConvertLength(value, unitFrom, unitTo);
            ViewData["Result"] = $"{value} {unitFrom} are {result} {unitTo}";
            return View();
        }

        private double ConvertLength(double value, string unitFrom, string unitTo)
        {
            if (unitFrom == unitTo)
                return value;

            double valueinMeters = unitFrom switch
            {
                "meters" => value,
                "kilometers" => value * 1000,
                "miles" => value * 1609.34,
                _ => throw new NotImplementedException()
            };

            return unitTo switch
            {
                "meters" => valueinMeters,
                "kilometers" => valueinMeters / 1000,
                "miles" => valueinMeters / 1609.34,
                _ => throw new NotImplementedException()
            };
        }

        public IActionResult Weight()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Weight(double value, string unitFrom, string unitTo)
        {
            double result = ConvertWeight(value, unitFrom, unitTo);
            ViewData["Result"] = $"{value} {unitFrom} are {result} {unitTo}";
            return View();
        }

        private double ConvertWeight(double value, string unitFrom, string unitTo)
        {
            if (unitFrom == unitTo)
                return value;

            double valueinGrams = unitFrom switch
            {
                "grams" => value,
                "kilograms" => value * 1000,
                "tons" => value * 1000000,
                _ => throw new NotImplementedException()
            };

            return unitTo switch
            {
                "grams" => valueinGrams,
                "kilograms" => valueinGrams / 1000,
                "tons" => valueinGrams / 1000000,
                _ => throw new NotImplementedException()
            };
        }

        public IActionResult Temperature()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Temperature(double value, string unitFrom, string unitTo)
        {
            double result = ConvertTemperature(value, unitFrom, unitTo);
            ViewData["Result"] = $"{value} {unitFrom} are {result} {unitTo}";
            return View();
        }

        private double ConvertTemperature(double value, string unitFrom, string unitTo)
        {
            if (unitFrom == unitTo)
                return value;

            double valueinCelsius = unitFrom switch
            {
                "celsius" => value,
                "farenheit" => (value * (5/9)) - 32,
                "kelvin" => value - 273.15,
                _ => throw new NotImplementedException()
            };

            return unitTo switch
            {
                "celsius" => valueinCelsius,
                "farenheit" => (valueinCelsius * (9 / 5)) + 32,
                "kelvin" => valueinCelsius + 273.15,
                _ => throw new NotImplementedException()
            };
        }



        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
