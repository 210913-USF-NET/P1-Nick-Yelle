using Models;
using DL;
using System.Collections.Generic;

namespace BL
{
    public class BrewLogic : IBrews
    {
        private IRepo _IRepo;

        public BrewLogic(IRepo repo)
        {
            _IRepo = repo;
        }
        public void AddBrew(Brew brew)
        {
            _IRepo.AddBrew(brew);
        }

        public List<Brew> GetAllBrews()
        {
            return _IRepo.GetAllBrews();
        }
    }
}

