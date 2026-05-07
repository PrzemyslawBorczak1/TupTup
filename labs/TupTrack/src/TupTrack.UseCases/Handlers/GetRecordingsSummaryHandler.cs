using System;
using System.Collections.Generic;
using System.Text;

using TupTrack.UseCases.Repositories;
using TupTrack.UseCases.DTOs;

namespace TupTrack.UseCases.Handlers
{
    public class GetRecordingsSummaryHandler
    {
        IStatisticsPageRepository _statisticsPageRepository;
        public GetRecordingsSummaryHandler(IStatisticsPageRepository statisticsPageRepository)
        {
            _statisticsPageRepository = statisticsPageRepository;
        }

        public async Task<List<RecordingSummaryDTO>> Handle()
        {
            return await _statisticsPageRepository.GetRecordingSummaries();
        }
    }
}
