using ApplicationCore.Contract.Service;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class CastController: Controller
    {
        private readonly ICastService _castService;

        public CastController(ICastService  castService)
        {
            _castService = castService;
        }

        // showing details of the movie
        public IActionResult CastDetail(int id)
        {
            var cast = _castService.GetCastDetails(id);
            return View(cast);
        }
    }
}
