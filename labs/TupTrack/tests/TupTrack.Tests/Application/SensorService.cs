using System;
using System.Linq;
using TupTrack.SensorServices;
using Xunit;

namespace TupTrack.Tests.Application
{
    public class SensorServiceTests
    {
        private sealed class TestSensorService : SensorService<int>
        {
            public TestSensorService(int tablesAmount = 100, int initializedAmount = 20, int tableSize = 1000)
                : base(tablesAmount, initializedAmount, tableSize)
            {
            }

            public void AddValue(int value) => Add(value);

            public void ClearValues() => Clear();
        }


        [Fact]
        public void SensorService_AfterOverflow_AdditionalAdds_ShouldNotChangeCount()
        {
            var sut = new TestSensorService(tablesAmount: 2, initializedAmount: 1, tableSize: 2);

            sut.AddValue(1);
            sut.AddValue(2);
            sut.AddValue(3);
            sut.AddValue(4);
            var countAfterFill = sut.Size;

            sut.AddValue(5);

            Assert.True(sut.overflow);
            Assert.Equal(4, countAfterFill);
            Assert.Equal(4, sut.Size);
            Assert.Equal(new[] { 1, 2, 3, 4 }, sut.Select(x => x.Item1).ToArray());
        }

        [Fact]
        public void SensorService_CrossingTableBoundary_ShouldKeepOrderAndCount()
        {
            var sut = new TestSensorService(tablesAmount: 3, initializedAmount: 1, tableSize: 2);

            sut.AddValue(11);
            sut.AddValue(22);
            sut.AddValue(33);

            var values = sut.Select(x => x.Item1).ToList();

            Assert.Equal(3, sut.Size);
            Assert.Equal(new[] { 11, 22, 33 }, values);
            Assert.False(sut.overflow);
        }

        [Fact]
        public void SensorService_Clear_ShouldResetCountAndEnumeration()
        {
            var sut = new TestSensorService(tablesAmount: 4, initializedAmount: 2, tableSize: 3);

            sut.AddValue(1);
            sut.AddValue(2);
            sut.AddValue(3);
            sut.ClearValues();

            Assert.Equal(0, sut.Size);
            Assert.Empty(sut);

            sut.AddValue(9);
            Assert.Equal(1, sut.Size);
            Assert.Equal(9, sut.Single().Item1);
        }

        [Fact]
        public void SensorService_Empty_ShouldHaveZeroCountAndNoItems()
        {
            var sut = new TestSensorService();

            Assert.Equal(0, sut.Size);
            Assert.False(sut.overflow);
            Assert.Empty(sut);
        }





        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(2, 1, 1)]
        public void SensorService_SmallSize_ShouldOverflow(int tablesAmount, int initializedAmount, int tableSize)
        {
            var sut = new TestSensorService(tablesAmount, initializedAmount, tableSize);
            var capacity = tablesAmount * tableSize;

            for (var i = 0; i < capacity; i++)
            {
                sut.AddValue(i);
            }

            var countAfterFill = sut.Size;
            sut.AddValue(999);

            Assert.True(sut.overflow);
            Assert.Equal(capacity, countAfterFill);
            Assert.Equal(capacity, sut.Size);
        }


        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(27)]
        public void SensorService_AddingN_Times_ShouldHaveSizeN(int n)
        {
            var sut = new TestSensorService(tablesAmount: 10, initializedAmount: 2, tableSize: 50);

            for (var i = 0; i < n; i++)
            {
                sut.AddValue(i);
            }

            Assert.Equal(n, sut.Size);
            Assert.False(sut.overflow);
        }

        [Theory]
        [InlineData(new[] { 10, 20, 30 })]
        [InlineData(new[] { 1, 2, 3, 4, 5 })]
        public void SensorService_AddingValues_ShouldEnumerableProperValues_WithDifferentDates(int[] values)
        {
            var sut = new TestSensorService(tablesAmount: 5, initializedAmount: 1, tableSize: 10);

            foreach (var value in values)
            {
                WaitForClockTick();
                sut.AddValue(value);
            }

            var enumerated = sut.ToList();
            var timestamps = enumerated.Select(x => x.Item2).ToList();

            Assert.Equal(values.Length, enumerated.Count);
            Assert.Equal(values, enumerated.Select(x => x.Item1));
            Assert.All(enumerated, item => Assert.NotEqual(default, item.Item2));
            Assert.True(timestamps.Distinct().Count() > 1);
            Assert.True(timestamps.Zip(timestamps.Skip(1), (a, b) => a <= b).All(x => x));
        }

     


        [Theory]
        [InlineData(0, 1, 1)]
        [InlineData(1, 0, 1)]
        [InlineData(1, 1, 0)]
        [InlineData(-1, 1, 1)]
        [InlineData(1, -1, 1)]
        [InlineData(1, 1, -1)]
        public void SensorService_InvalidConstructorArguments_ShouldThrowArgumentException(int tablesAmount, int initializedAmount, int tableSize)
        {
            Assert.Throws<ArgumentException>(() => new TestSensorService(tablesAmount, initializedAmount, tableSize));
        }


        private static void WaitForClockTick()
        {
            var start = DateTime.Now;
            while (DateTime.Now == start)
            {
            }
        }
    }
}
