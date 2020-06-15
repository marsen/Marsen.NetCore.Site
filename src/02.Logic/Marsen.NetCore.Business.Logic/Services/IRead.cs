using System;
using System.Collections.Generic;
using System.Text;

namespace Marsen.Business.Logic.Services
{
    public interface IRead<T>
    {
        T Read(long id);
    }
}
