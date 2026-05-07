using System;
using System.Collections.Generic;
using System.Text;
using TupTrack.Infrastructure.Tables;
using TupTrack.UseCases.DTOs;
using TupTrack.UseCases.Repositories;

namespace TupTrack.Infrastructure.Repositories
{
    public class StatisticsPageRepository : IStatisticsPageRepository
    {
        DatabaseContext _databaseContext;

        public StatisticsPageRepository(DatabaseContext databaseContext)
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
