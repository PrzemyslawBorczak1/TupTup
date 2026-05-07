using System;
using System.Collections.Generic;
using System.Text;
using TupTrack.UseCases.DTOs;

namespace TupTrack.UseCases.Repositories
{
    public interface IStatisticsPageRepository
    {
        Task<List<RecordingSummaryDTO>> GetRecordingSummaries();
    }
}
