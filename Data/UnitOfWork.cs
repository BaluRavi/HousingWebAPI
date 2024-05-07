using HousingWebAPI.Data.Interfaces;
using HousingWebAPI.Data.Repo;

namespace HousingWebAPI.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDataContext dataContext;

        public UnitOfWork(AppDataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public ICityRepository CityRepository => new CityRepository(dataContext);

        public async Task<bool> SaveAsync()
        {
            return await dataContext.SaveChangesAsync() > 0;
        }
    }
}
