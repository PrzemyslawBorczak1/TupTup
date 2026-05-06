using System;
using System.Threading.Tasks;
using TupTrack.Domain;
using TupTrack.Domain.Entities;
using TupTrack.UseCases.DTOs;
using TupTrack.UseCases.Handlers;
using TupTrack.UseCases.Repositories;
using TupTrack.UseCases.SensorCoordinator;
using Xunit;

namespace TupTrack.Tests.Application
{
    public class StartRecordingHandlerTests
    {
        private sealed class FakeRecordingRepository : IRecordingRepository
        {
            public Recording? Recording { get; private set; }
            public TupStateEntity? TupState { get; private set; }
            public RoomTimestamp? RoomTimestamp { get; private set; }
            public Guid? FailedRecordingId { get; private set; }
            public string? FailureReason { get; private set; }

            public Room RoomToReturn { get; set; } = new Room("Room1", null);

            public Task AddInitialRecording(Recording recording, TupStateEntity tupStateEntity, RoomTimestamp roomTimestamp)
            {
                Recording = recording;
                TupState = tupStateEntity;
                RoomTimestamp = roomTimestamp;
                return Task.CompletedTask;
            }

            public Task MarkAsFailed(Guid recordingId, string? failureReason)
            {
                FailedRecordingId = recordingId;
                FailureReason = failureReason;
                return Task.CompletedTask;
            }

            public Task<Room> GetRoomAsync(string roomName) => Task.FromResult(RoomToReturn);
        }

        private sealed class FakeSensorCoordinator : ISensorCoordinator
        {
            public SensorSpeed? SpeedSet { get; private set; }
            public bool Started { get; private set; }
            public bool ThrowOnStart { get; set; }

            public void SetSpeed(SensorSpeed speed) => SpeedSet = speed;

            public void Start()
            {
                if (ThrowOnStart)
                    throw new InvalidOperationException("sensor-failed");

                Started = true;
            }

            public void Stop() { }

            public void Dispose() { }
        }

        [Fact]
        public async Task Handle_WhenRoomIsEmpty_ShouldThrow()
        {
            var repo = new FakeRecordingRepository();
            var sensors = new FakeSensorCoordinator();
            var sut = new StartRecordingHandler(sensors, repo);

            await Assert.ThrowsAsync<ArgumentException>(() =>
                sut.Handle(new StartRecordingDTO
                {
                    Room = "",
                    StartTime = DateTime.UtcNow
                }));
        }

        [Fact]
        public async Task Handle_ShouldPersistAndStartSensors()
        {
            var repo = new FakeRecordingRepository();
            var sensors = new FakeSensorCoordinator();
            var sut = new StartRecordingHandler(sensors, repo);

            var id = await sut.Handle(new StartRecordingDTO
            {
                Room = "Room1",
                StartTime = DateTime.UtcNow,
                FirstTupState = TupState.Flat,
                SensorSpeed = SensorSpeed.Fast
            });

            Assert.NotEqual(Guid.Empty, id);
            Assert.NotNull(repo.Recording);
            Assert.NotNull(repo.TupState);
            Assert.NotNull(repo.RoomTimestamp);

            Assert.Equal(id, repo.Recording!.Id);
            Assert.Equal(id, repo.TupState!.RecordingId);
            Assert.Equal(id, repo.RoomTimestamp!.RecordingId);

            Assert.True(sensors.Started);
            Assert.Equal(SensorSpeed.Fast, sensors.SpeedSet);
        }

        [Fact]
        public async Task Handle_WhenSensorStartFails_ShouldMarkAsFailed()
        {
            var repo = new FakeRecordingRepository();
            var sensors = new FakeSensorCoordinator { ThrowOnStart = true };
            var sut = new StartRecordingHandler(sensors, repo);

            var id = await sut.Handle(new StartRecordingDTO
            {
                Room = "Room1",
                StartTime = DateTime.UtcNow,
                FirstTupState = TupState.Flat,
                SensorSpeed = SensorSpeed.Fast
            });

            Assert.Equal(id, repo.FailedRecordingId);
            Assert.Equal("sensor-failed", repo.FailureReason);
        }
    }
}
