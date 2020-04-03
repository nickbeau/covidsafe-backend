﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using TraceDefense.DAL.Providers;
using TraceDefense.Entities.Protos;

namespace TraceDefense.DAL.Repositories.Mock
{
    /// <summary>
    /// Mock implemnentation of <see cref="IProximityQueryRepository"/>
    /// </summary>
    public class MockMessageRepository : IMessageRepository
    {
        public Task<MatchMessage> GetAsync(string id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MessageInfo>> GetLatestAsync(Location locmin, Location locmax, long lastTimestamp, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<long> GetLatestRegionSizeAsync(Location locmin, Location locmax, long lastTimestamp, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MatchMessage>> GetRangeAsync(IEnumerable<string> ids, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(Location locmin, Location locmax, MatchMessage record, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}