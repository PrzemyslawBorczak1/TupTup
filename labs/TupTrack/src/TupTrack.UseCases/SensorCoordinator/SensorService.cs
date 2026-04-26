using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;


namespace TupTrack.SensorServices
{
    public abstract class SensorService<T> : IEnumerable<(T,DateTime)>
    {
        private int TABLES_AMOUNT;
        private int INITIALIZED_AMOUNT;
        private int TABLE_SIZE;


        private (T, DateTime)[][] _tables;
        private int activeTable = 0;
        private int activeTableIndex = 0;


        private bool firstInitializedReached = false;
        public bool Overflow { get; private set; } = false;

        public int Size
        {
            get => activeTable * TABLE_SIZE + activeTableIndex;
        }

        protected SensorService(int TABLES_AMOUNT = 100, int INITIALIZED_AMOUNT = 20, int TABLE_SIZE = 1000)
        {
            INITIALIZED_AMOUNT = Math.Min(INITIALIZED_AMOUNT, TABLES_AMOUNT);
            if (TABLES_AMOUNT <= 0 || INITIALIZED_AMOUNT <= 0 || TABLE_SIZE <= 0)
                throw new ArgumentException("Values in constructor must be positive");

            this.TABLES_AMOUNT = TABLES_AMOUNT;
            this.INITIALIZED_AMOUNT = INITIALIZED_AMOUNT;
            this.TABLE_SIZE = TABLE_SIZE;

            _tables = new (T, DateTime)[TABLES_AMOUNT][];
            for (int i = 0; i < INITIALIZED_AMOUNT; i++)
            {
                _tables[i] = new (T, DateTime)[TABLE_SIZE];
            }
        }

        protected void Add(T val)
        {
            var time = DateTime.Now;
            lock (this)
            {
                if (Overflow)
                    return;

                _tables[activeTable][activeTableIndex] = (val, time);

                activeTableIndex++;

                ProgressIndex();
            }
           
        }

        private void ProgressIndex()
        {
            if (activeTable == TABLES_AMOUNT - 1 && activeTableIndex == TABLE_SIZE)
            {
                Overflow = true;
                return;
            }

            if (activeTableIndex == TABLE_SIZE)
            {
                activeTableIndex = 0;
                activeTable++;

                if (!firstInitializedReached && activeTable == INITIALIZED_AMOUNT)
                    firstInitializedReached = true;

                if (firstInitializedReached)
                    _tables[activeTable] = new (T, DateTime)[TABLE_SIZE];

            }
        }

        protected void Clear()
        {
            if (activeTable >= INITIALIZED_AMOUNT)
            {
                var tablesToClear = activeTable - INITIALIZED_AMOUNT + 1;
                Array.Clear(_tables, INITIALIZED_AMOUNT, tablesToClear);
            }
            activeTable = 0;
            activeTableIndex = 0;
            firstInitializedReached = false;
            overflow = false;

        }

        public IEnumerator<(T, DateTime)> GetEnumerator()
        {
            for(int i = 0; i < activeTable; i++)
            {
                for(int j = 0; j < TABLE_SIZE; j++)
                {
                    yield return _tables[i][j];
                }
            }

            for (int i = 0; i < activeTableIndex; i++)
            {
                yield return _tables[activeTable][i];
            }

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
