using Microsoft.AspNetCore.Http;

namespace BloodDonation.Application.IntegrationServices.Interfaces;

public interface IFirebaseService
{
    Task<Firebase.Auth.User> GetUserAsync(string email, string password);
    Task<Firebase.Auth.User> SignUpAsync(string email, string password);
    Task<(string FileName, string Url)> SaveFileAsync(IFormFile file, string directory);



}
