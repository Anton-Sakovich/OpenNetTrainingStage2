using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonkeyBanker.Web.Models
{
    public class CrudDetailsViewModel<TEntity>
    {
        public TEntity Entity { get; set; }
    }
}