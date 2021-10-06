using System;
using Models;
using System.Collections.Generic;

namespace BL
{
    public interface IBrews
    {
        void AddBrew(Brew brew);
        List<Brew> GetAllBrews();
    }
}
