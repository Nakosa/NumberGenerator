using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace NumberGenerator
{
    public class NumberGenerator
    {

        private struct IntermediateResult
        {
            public bool isAlive;
            public object data;
            public DateTime Start, End;
            public int diff;
            public bool worked;

            public IntermediateResult(DateTime s) {
                this.isAlive = true;
                this.data = null;
                this.Start = this.End = s;
                this.diff = 0;
                this.worked = true;
            }

            public void complete(object d, DateTime e, bool w = true)
            {
                this.data = d;
                this.End = e;
                this.worked = w;
                this.diff = (int)(this.End - this.Start).TotalMilliseconds;
                this.isAlive = false;
            }

        }

        public struct Result
        {
            public object data;
            public int MainDiff;
            public int[] ThreadDiffs;

            private DateTime Start, End;

            public Result(DateTime s)
            {
                this.Start = this.End = s;
                this.data = null;
                this.MainDiff = 0;
                this.ThreadDiffs = new int[0];
            }

            public void complete(object d, DateTime e, int[] td) {
                this.data = d;
                this.End = e;
                this.MainDiff = (int)(this.End - this.Start).TotalMilliseconds;
                this.ThreadDiffs = td;
            }
        }

        private int countNumbers;

        private int[] local_arrayNumbers;

        public Result generateResult;
        private IntermediateResult[] generateIntermediateResult;
        private int genThreads;
        private int genH;


        private int local_MaxValue;
        private int[] local_diffMax;

        public Result calculateResult;
        private IntermediateResult[] calculateIntermediateResult;
        private int calcThreads;
        private int calcH;


        private int msChecker = 1;
        private int sleepChecker = 50;

        public bool isGenerate;
        public bool isCalculate;

        public NumberGenerator(int numbers, int threads) {

            this.countNumbers = numbers;

            this.genThreads = threads;
            this.genH = this.getHeight(this.countNumbers, this.genThreads);

            this.local_arrayNumbers = new int[this.countNumbers];

            this.Generate();
        }

        public void foundMax(int threads) {
            this.isCalculate = true;
            this.calculateResult = new Result(DateTime.Now);
            this.calcThreads = threads;
            this.calcH = this.getHeight(this.countNumbers, this.calcThreads);
            this.local_MaxValue = 0;
            this.local_diffMax = new int[this.calcThreads];

            Thread[] arrThreads = new Thread[this.calcThreads];
            this.calculateIntermediateResult = new IntermediateResult[this.calcThreads];
            for (int i = 0; i < this.calcThreads; i++)
            {
                int index = i;
                arrThreads[i] = new Thread(() => this.Max(index));
                arrThreads[i].IsBackground = true;
                arrThreads[i].Start();
            }

            Thread.Sleep(this.sleepChecker);
            while (!Array.TrueForAll(this.calculateIntermediateResult, val => val.isAlive == false))
            {
                Thread.Sleep(this.msChecker);
            }
            int localMax;
            List<int> listDiff = new List<int>();
            foreach (IntermediateResult x in this.calculateIntermediateResult)
            {
                localMax = (int)x.data;
                if (this.local_MaxValue < localMax) {
                    this.local_MaxValue = localMax;
                }
                if (x.worked) {
                    listDiff.Add(x.diff);
                }
                else
                {
                    listDiff.Add(-1);
                }
            }
            this.local_diffMax = listDiff.ToArray();

            this.calculateResult.complete(this.local_MaxValue, DateTime.Now, this.local_diffMax);
            int realMax = (this.generateResult.data as int[]).Max();

            int countMaxNumbers = 0;
            foreach (int x in this.local_arrayNumbers) {
                if (x == realMax) {
                    countMaxNumbers++;
                }
            }

            this.isCalculate = false;
        }

        private void Max(int calculateIndex) {
            int index = calculateIndex;
            this.calculateIntermediateResult[index] = new IntermediateResult(DateTime.Now);
            int arrLen = this.local_arrayNumbers.Length - 1;
            int Return = -1;
            int startIndex = this.getStartIndex(this.calcH, index);
            int endIndex = this.getLastIndex(startIndex, this.calcH, arrLen);
            for (int i = startIndex; i <= endIndex; i++) {
                if (Return < this.local_arrayNumbers[i])
                {
                    Return = this.local_arrayNumbers[i];
                }
            }
            bool worked;
            if (Return > -1) {
                worked = true;
            }
            else
            {
                worked = false;
            }

            this.calculateIntermediateResult[index].complete(Return, DateTime.Now, worked);
        }






        private void Generate() {
            this.isGenerate = true;
            this.generateResult = new Result(DateTime.Now);
            Thread[] arrThreads = new Thread[this.genThreads];
            this.generateIntermediateResult = new IntermediateResult[this.genThreads];
            for (int i = 0; i < this.genThreads; i++)
            {
                int index = i;
                arrThreads[i] = new Thread(() => this.generate(index));
                arrThreads[i].IsBackground = true;
                arrThreads[i].Start();
            }

            Thread.Sleep(this.sleepChecker);
            while (!Array.TrueForAll(this.generateIntermediateResult, val => val.isAlive == false))
            {
                Thread.Sleep(this.msChecker);
            }
            arrThreads = null;
            List<int> listNumbers = new List<int>();
            List<Int32> listDiff = new List<Int32>();
            foreach (IntermediateResult x in this.generateIntermediateResult)
            {
                listNumbers.AddRange((x.data as int[]).ToList());
                if (x.worked)
                {
                    listDiff.Add(x.diff);
                }
                else
                {
                    listDiff.Add(-1);
                }
            }
            this.local_arrayNumbers = listNumbers.ToArray();
            listNumbers = null;
            
            this.generateResult.complete(this.local_arrayNumbers, DateTime.Now, listDiff.ToArray());
            listDiff = null;

            this.generateIntermediateResult = null;
 
            this.isGenerate = false;
        }

        private void generate(int generateIndex) {
            List<int> Return = new List<int>();
            int index = generateIndex;
            this.generateIntermediateResult[index] = new IntermediateResult(DateTime.Now);
            //Random rnd = new Random((int)DateTime.Now.Ticks);
            RealRandomGenerator rnd = new RealRandomGenerator();
            int returnLen = this.getReturnLen(index, this.genH, this.local_arrayNumbers.Length - 1);
            lock (Return) {
                for (int i = 0; i < returnLen; i++)
                {
                    //Return.Add(rnd.Next());
                    Return.Add((int)rnd.NextInt());
                    //Thread.Sleep(1);
                }
                bool worked;
                if (Return.Count > 0) {
                    worked = true;
                }
                else
                {
                    worked = false;
                }
                this.generateIntermediateResult[index].complete(Return.ToArray(), DateTime.Now, worked);
            }
        }










        private int getReturnLen(int i, int h, int arrLen) {
            int start = this.getStartIndex(h, i);
            int end = this.getLastIndex(start, h, arrLen);
            if (start > end) {
                return 0;
            }
            return end - start + 1;
        }

        private int getStartIndex(int h, int index)
        {
            return h * index;
        }

        private int getLastIndex(int start, int h, int len) {
            int end = start + h - 1;
            if (end > len)
            {
                end = len;
            }
            return end;
        }

        private int getHeight(int count, int threads) {
            return (int)Math.Ceiling((double)count/threads);
        }
    }
}
