using CityGuide.Models;

namespace CityGuide.Data
{
    public interface IAppRepository
    {
        void Add<T>(T entity) where T:class;
        void Delete<T>(T entity) where T : class;
        bool SaveAll();
        List<City> GetCities();
        List<Photo> GetPhotosByCity(int cityId);
        City GetCity(int cityId);
        Photo GetPhoto(int id);
    }
}
