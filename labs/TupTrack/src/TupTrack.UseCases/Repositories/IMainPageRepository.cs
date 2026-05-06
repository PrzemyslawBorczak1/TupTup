using System;
using System.Collections.Generic;
using System.Text;

namespace TupTrack.UseCases.Repositories
{
    public interface IMainPageRepository
    {

        public Task<List<string>> GetAllRoomNamesAsync();

        public Task<List<string>> GetAllRecordingGroupNamesAsync();

    }
}
