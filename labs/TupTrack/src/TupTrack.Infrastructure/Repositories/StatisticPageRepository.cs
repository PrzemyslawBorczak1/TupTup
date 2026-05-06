using System;
using System.Collections.Generic;
using System.Text;

using TupTrack.UseCases.DTOs;
using TupTrack.Infrastructure.Tables;

namespace TupTrack.Infrastructure.Repositories
{
    public class StatisticPageRepository
    {
        DatabaseContext _databaseContext;

        public StatisticPageRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<RecordingSummaryDTO>> GetRecordingSummaries()
        {
            var recordings = await _databaseContext.Connection.Table<Recording>().ToListAsync();
            return recordings.Select(r => new RecordingSummaryDTO()
            {
                Id = r.Id,
                StartTime = r.StartTime,
                State = r.State,
                EndTime = r.EndTime,
                GroupName = r.GroupName,
            }).ToList();

        }
    }
}
