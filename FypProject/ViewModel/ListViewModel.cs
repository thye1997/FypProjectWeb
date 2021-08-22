using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FypProject.ViewModel
{
    public abstract class ListViewModel<T> where T: class
    {
        public abstract List<T> DataList { get; set; }
    }
}
