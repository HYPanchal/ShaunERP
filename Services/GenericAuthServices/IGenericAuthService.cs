using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.GenericAuthServices
{
    public interface IGenericAuthService<T>
    {
        Task<string> AuthenticateAsync(T obj);
    }
}
