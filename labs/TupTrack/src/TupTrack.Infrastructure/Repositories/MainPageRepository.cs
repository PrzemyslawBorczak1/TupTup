using System;
using System.Collections.Generic;
using System.Text;

using TupTrack.UseCases.Repositories;

namespace TupTrack.Infrastructure.Repositories
{
    public class MainPageRepository : IMainPageRepository
    {
        private readonly DatabaseContext _databaseContext;

        public MainPageRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }


        public async Task<List<string>> GetAllRoomNamesAsync()
        {
            await _databaseContext.InitAsync();
            var rooms = await _databaseContext.Connection.Table<Tables.Room>().ToListAsync();
            return rooms.Select(r => r.Name).ToList();
        }

        public async Task<List<string>> GetAllRecordingGroupNamesAsync()
        {
            await _databaseContext.InitAsync();
            var groups = await _databaseContext.Connection.Table<Tables.RecordingGroup>().ToListAsync();
            return groups.Select(g => g.Name).ToList();
        }


    }
}
