using TestProject.WebAPI.Data.Db;
using TestProject.WebAPI.Models;

namespace TestProject.WebAPI.Profiles
{
    public class ZipPayProfile : AutoMapper.Profile
    {
        public ZipPayProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();
            CreateMap<Account, AccountModel>().
                AfterMap((src, dest, context) => dest.User = context.Mapper.Map<User, UserModel>(src.User));
        }
    }
}
