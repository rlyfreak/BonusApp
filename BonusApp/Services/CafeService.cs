using BonusApp.Models;

namespace BonusApp.Services
{
    public class CafeService
    {
        private static readonly CafeService _instance = new CafeService();
        public static CafeService Instance => _instance;
        private CafeService() { }
        public List<Cafe> GetCafes()
        {
            return CafeCatalog.GetAll()
                .Select(entry => new Cafe
                {
                    ID = entry.Id,
                    Name = entry.Name
                })
                .ToList();
        }
        public Cafe? GetCafeById(int id)
        {
            var cafe = CafeCatalog.GetById(id);
            return cafe == null
                ? null
                : new Cafe
                {
                    ID = cafe.Id,
                    Name = cafe.Name
                };
        }
    }
}
