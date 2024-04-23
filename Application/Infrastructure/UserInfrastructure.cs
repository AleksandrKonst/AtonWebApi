using Infrastructure.Data.Context;
using Infrastructure.Repositories;

namespace Application.Infrastructure;

public class UserInfrastructure
{
    public static async Task CheckUser(string login, string userLogin)
    {
        var repository = new UserRepository(new AtonDataContext());
        
        var currentUser = await repository.GetAsync(userLogin);
        if (currentUser == null || currentUser.RevokedOn != null || (currentUser.Admin == false && currentUser.Login != login))
            throw new Exception("Ошибка доступа");
    }
}