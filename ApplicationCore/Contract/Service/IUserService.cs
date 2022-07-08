﻿using ApplicationCore.Entity;
using ApplicationCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ApplicationCore.Contract.Service
{
    public interface IUserService
    {
        Task<bool> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId);
        Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId);
        Task<IEnumerable<PurchaseRequestModel>> GetAllPurchasesForUserId(int id);
        Task<bool> AddFavorite(FavoriteRequestModel favoriteRequest);
        Task<bool> RemoveFavorite(FavoriteRequestModel favoriteRequest);
        Task<bool> FavoriteExists(int id, int movieId);
        Task<Favorite> GetFavoriteById(int userId, int movieId);
        Task<IEnumerable<MovieCardModel>> GetAllFavoritesForUser(int id);
        Task<bool> AddMovieReview(ReviewRequestModel reviewRequest);
        Task<ReviewRequestModel> GetReview(int userId, int movieId);
        Task<bool> UpdateMovieReview(ReviewRequestModel reviewRequest);
        Task<bool> DeleteMovieReview(int userId, int movieId);
        //Task<> GetAllReviewsByUser(int id);
        
    }
}
