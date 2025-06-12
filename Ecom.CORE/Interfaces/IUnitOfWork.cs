using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.CORE.Interfaces
{
    public interface IUnitOfWork
    {
        public ICategoryRepositiry CategoryRepositiry { get;  }
        public IPhotoRepositiry PhotoRepositiry { get;  }
        public IProductRepositiry ProductRepositiry { get;  }

    }
}
