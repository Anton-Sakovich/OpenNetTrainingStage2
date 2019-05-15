using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MonkeyBanker.Entities;

namespace MonkeyBanker.Web.Models.People
{
    public class IndexViewModel
    {
        public IEnumerable<Person> People { get; set; }
    }
}