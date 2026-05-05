using System;
using System.Collections.Generic;
using System.Text;  


using TupTrack.UseCases.Repositories;
using TupTrack.UseCases.DTOs;

namespace TupTrack.UseCases.Handlers
{
    public class GetRecordingOptionsHandler
    {

        IMainPageRepository _mainPageRepository;

       
        public GetRecordingOptionsHandler(IMainPageRepository mainPageRepository)
        {
            _mainPageRepository = mainPageRepository;
        }

        public async Task<RecordingOptionsDTO> Handle()
        {
            var rooms = await _mainPageRepository.GetAllRoomNamesAsync();
            var groups = await _mainPageRepository.GetAllRecordingGroupNamesAsync();
            return new RecordingOptionsDTO
            {
                Rooms = rooms,
                Groups = groups
            };
        }
    }
}
