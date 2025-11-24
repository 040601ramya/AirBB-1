using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AirBB.Models.DataLayer
{
    public class QueryOptions<T>
    {
        public Expression<Func<T, bool>>? Where { get; set; }
        public Expression<Func<T, object>>? OrderBy { get; set; }
        public bool OrderByDescending { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; }
            = new List<Expression<Func<T, object>>>();
    }
}
