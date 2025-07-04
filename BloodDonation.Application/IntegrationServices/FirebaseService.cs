using BloodDonation.Application.IntegrationServices.Interfaces;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;

namespace BloodDonation.Application.IntegrationServices
{
    public class FirebaseService : IFirebaseService
    {
        private FirebaseAuthProvider authProvider;
        private FirebaseAuthLink ServiceAccount { get; set; }
        private FirebaseStorage FirebaseStorage;
        public FirebaseService(AppSetting appSetting)
        {

            authProvider = new FirebaseAuthProvider(new FirebaseConfig(apiKey: appSetting.FirebaseConfig.ApiKey));
            ServiceAccount = authProvider.SignInWithEmailAndPasswordAsync(appSetting.FirebaseConfig.ServiceAccountUsr, appSetting.FirebaseConfig.ServiceAccountPwd).Result;
            FirebaseStorage = new FirebaseStorage(
                    appSetting.FirebaseConfig.Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(ServiceAccount.FirebaseToken),
                        ThrowOnCancel = false
                    });
        }
        public async Task<User> GetUserAsync(string email, string password)
        {
            var authResult = await authProvider.SignInWithEmailAndPasswordAsync(email, password);
            if (authResult is not null)
            {
                return authResult.User;
            }
            else throw new InvalidOperationException("Sign in with Firebase Failed");
        }


        public async Task<(string FileName, string Url)> SaveFileAsync(IFormFile file, string directory)
        {
            if (file.Length > 0)
            {
                try
                {
                    var fs = file.OpenReadStream();
                    var auth = authProvider;

                    var a = ServiceAccount;
                    Console.Write(a.FirebaseToken);
                    string name = file.FileName + $"-{Guid.NewGuid()}";


                    var cancellation = FirebaseStorage.Child("assets/" + directory).Child(name)
                        .PutAsync(fs, CancellationToken.None);

                    var result = await cancellation;
                    return (file.FileName, result);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);

                }

            }
            else throw new Exception("File is not existed!");
        }

        public async Task<User> SignUpAsync(string email, string password)
        {
            var res = await authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
            if (res.User is not null)
            {
                return res.User;
            }
            else throw new InvalidOperationException("Something went wrong with firebase auth");
        }
    }
}
