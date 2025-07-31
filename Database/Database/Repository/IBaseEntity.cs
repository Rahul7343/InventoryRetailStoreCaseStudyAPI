using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repository
{
    public interface IBaseEntity
    {
        long Id { get; }
        bool IsDeleted { get; }
    }
}
