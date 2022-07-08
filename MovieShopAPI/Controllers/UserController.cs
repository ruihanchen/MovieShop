using ApplicationCore.Contract.Service;
using ApplicationCore.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //[HttpGet]
        //[Route("purchases")]
        //public async Task<IActionResult> GetMoviesPurchsaedUser()
        //{
        //    // we need to get the userId from the token, using HttpContext
        //    return Ok();
        //}

        //[HttpGet]
        //[Route("details")]
        //public async Task<IActionResult> GetUserDetails()
        //{
        //    var userDetails = await _userService.GetById();
        //}

        //[HttpPost]
        //[Route("purchase-movie")]
        //public async Task<IActionResult> BuyMovies(PurchaseRequestModel purchaseRequest, int userId)
        //{
        //    var purchase = await _userService.PurchaseMovie(purchaseRequest, userId);
        //    return Ok(purchase);
        //}
        [HttpPost]
        [Route("favorite")]
        public async Task<IActionResult> AddFavorite([FromBody] FavoriteRequestModel favoriteRequest)
        {
            var favorite = await _userService.AddFavorite(favoriteRequest);
            return Ok(favorite);
        }

        [HttpPost]
        [Route("un-favorite")]
        public async Task<IActionResult> removieFavorite(FavoriteRequestModel favoriteRequest)
        {
            var unfavorite = await _userService.RemoveFavorite(favoriteRequest);
            return Ok(unfavorite);
        }
        [HttpGet]
        [Route("check-movie-favorite/{movieId:int}")]
        public async Task<IActionResult> FavoriteExists(int id, int movieId)
        {
            var favoriteExists = await _userService.FavoriteExists(id, movieId);
            return Ok(favoriteExists);
        }
        [HttpPost]
        [Route("add-review")]
        public async Task<IActionResult> AddMovieReview(ReviewRequestModel reviewRequest)
        {
            var addReview = await _userService.UpdateMovieReview(reviewRequest);
            return Ok(addReview);
        }
        [HttpPut]
        [Route("edit-review")]
        public async Task<IActionResult> UpdateMovieReview(ReviewRequestModel reviewRequest)
        {
            var updateReview = await _userService.UpdateMovieReview(reviewRequest);
            return Ok(updateReview);
        }
        [HttpDelete]
        [Route("delete-review/{movieId:int}")]
        public async Task<IActionResult> DeleteMovieReview(int userId, int movieId)
        {
            var deleteReview = await _userService.DeleteMovieReview(userId, movieId);
            return Ok(deleteReview);
        }
        [HttpGet]
        [Route("purchases")]
        public async Task<IActionResult> GetAllPurchasesForUserId(int id)
        {
            var purchaseByUser = await _userService.GetAllPurchasesForUserId(id);
            return Ok(purchaseByUser);
        }

        [HttpGet]
        [Route("purchase-details/{movieId:int}")]
        public async Task<IActionResult> GetPurchaseDetails()
        {
            var purchasreDetails = await _userService.GetPurchaseDetails();
            return Ok(purchasreDetails);
        }
        [HttpGet]
        [Route("check-movie-purchased/{movieId:int}")]
        public async Task<IActionResult> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId)
        {
            var purchased = await _userService.IsMoviePurchased(purchaseRequest, userId);
            return Ok(purchased);
        }
    }
}
