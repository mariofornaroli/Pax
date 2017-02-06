using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ResultModel<T> : BaseResultModel
    {
        public T ResultData { get; set; }
    }
}
