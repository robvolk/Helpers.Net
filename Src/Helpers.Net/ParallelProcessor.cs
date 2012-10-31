using System.Collections.Generic;
using System.Linq;

namespace System.Threading
{
    public static class ParallelProcessor
    {
        /// <summary>
        /// Enumerates through each item in a list in parallel
        /// </summary>
        public static void EachParallel<T>(this IEnumerable<T> list, Action<T> action)
        {
            // enumerate the list so it can't change during execution
            // TODO: why is this happening?
            list = list.ToArray();
            var count = list.Count();

            if (count == 0)
            {
                return;
            }
            else if (count == 1)
            {
                // if there's only one element, just execute it
                action(list.First());
            }
            else
            {
                // Launch each method in it's own thread
                const int MaxHandles = 64;
                for (var offset = 0; offset <= count / MaxHandles; offset++)
                {
                    // break up the list into 64-item chunks because of a limitiation in WaitHandle
                    var chunk = list.Skip(offset * MaxHandles).Take(MaxHandles);

                    // Initialize the reset events to keep track of completed threads
                    var resetEvents = new ManualResetEvent[chunk.Count()];

                    // spawn a thread for each item in the chunk
                    int i = 0;
                    foreach (var item in chunk)
                    {
                        resetEvents[i] = new ManualResetEvent(false);
                        ThreadPool.QueueUserWorkItem(new WaitCallback((object data) =>
                        {
                            int methodIndex = (int)((object[])data)[0];

                            // Execute the method and pass in the enumerated item
                            action((T)((object[])data)[1]);

                            // Tell the calling thread that we're done
                            resetEvents[methodIndex].Set();
                        }), new object[] { i, item });
                        i++;
                    }

                    // Wait for all threads to execute
                    WaitHandle.WaitAll(resetEvents);
                }
            }
        }

        /// <summary>
        /// Executes a set of methods in parallel and returns the results
        /// from each in an array when all threads have completed.  The methods
        /// must take no parameters and have no return value.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static void ExecuteParallel(params Action[] actions)
        {
            // Initialize the reset events to keep track of completed threads
            ManualResetEvent[] resetEvents = new ManualResetEvent[actions.Length];

            // Launch each method in it's own thread
            for (int i = 0; i < actions.Length; i++)
            {
                resetEvents[i] = new ManualResetEvent(false);
                ThreadPool.QueueUserWorkItem(new WaitCallback((object index) =>
                {
                    int actionIndex = (int)index;

                    // Execute the method
                    actions[actionIndex]();

                    // Tell the calling thread that we're done
                    resetEvents[actionIndex].Set();
                }), i);
            }

            // Wait for all threads to execute
            WaitHandle.WaitAll(resetEvents);
        }
    }
}
