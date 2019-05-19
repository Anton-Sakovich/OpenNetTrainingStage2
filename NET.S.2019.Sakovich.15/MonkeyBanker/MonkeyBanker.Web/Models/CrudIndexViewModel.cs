using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonkeyBanker.Web.Models
{
    public class CrudIndexViewModel<T>
    {
        public IEnumerable<T> Entitites;
    }
}