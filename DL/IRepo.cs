using System;
using Models;
using System.Collections.Generic;

namespace DL
{
    public interface IRepo
    {
        void AddBrew(Brew brew);

        List<Brew> GetAllBrews();
    }
}
