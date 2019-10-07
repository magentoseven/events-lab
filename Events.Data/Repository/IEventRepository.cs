using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Events.Data.Repository
{
    public interface IEventRepository : IGenericRepository<Event>
    {
        IEnumerable<TResult> GetPublicEvents<TResult>(Expression<Func<Event, TResult>> selector);
    }
}