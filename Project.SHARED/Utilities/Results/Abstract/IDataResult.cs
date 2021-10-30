using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.SHARED.Utilities.Results.Abstract
{
    public interface IDataResult<out T>:IResult //tek bir prop içerisinde istersek liste istersek tek bir entity taşıyabilmek için out type kullandık...
    {
        public T Data { get; }
        // new DataResult<Category>(ResultStatus.Success, category); 
        // new DataResult<IList<Category>>(ResultStatus.Success, categoryList); 
    }
}
