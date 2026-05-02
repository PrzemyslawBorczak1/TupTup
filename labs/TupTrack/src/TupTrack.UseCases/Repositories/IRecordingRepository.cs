using Entities = TupTrack.Domain.Entities;

namespace TupTrack.UseCases.Repositories
{
    public interface IRecordingRepository
    {

        public Task AddRecording(Entities.Recording recording);

        public Task AddTupState(Entities.TupStateEntity tupStateEntity);
    }
}
