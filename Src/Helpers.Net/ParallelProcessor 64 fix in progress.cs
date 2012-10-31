using System.Collections.Generic;
using System.Linq;

namespace System.Threading
{
    public static class ParallelProcessor
    {
        /// <summary>
        /// Executes an action on each element of an enumerable in their own threads
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="action"></param>
        public static void EachParallel<T>(this IList<T> list, Action<T> action)
        {
            list.EachParallel(action, 0);
        }

        /// <summary>
        /// Executes an action on each element of an enumerable in their own threads
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="action"></param>
        /// <param name="sleep">Time in miliseconds to sleep between spawning new threads</param>
        public static void EachParallel<T>(this IList<T> list, Action<T> action, int sleep)
        {
            const int MaxWaitHandles = 64;

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
                for (int i = 0; i < count; i += MaxWaitHandles)
                {
                    // chunk the list into 64 threads at a time
                    var max = i + MaxWaitHandles > count ? count % MaxWaitHandles : MaxWaitHandles;

                    // Initialize the reset events to keep track of completed threads
                    ManualResetEvent[] resetEvents = new ManualResetEvent[max];

                    // Launch each method in it's own thread
                    for (int j = i; j < max; j++)
                    {
                        resetEvents[j] = new ManualResetEvent(false);
                        ThreadPool.QueueUserWorkItem(new WaitCallback((object data) =>
                        {
                            int methodIndex = (int)((object[])data)[0];

                            // Execute the method and pass in the enumerated item
                            action((T)((object[])data)[1]);

                            // Tell the calling thread that we're done
                            resetEvents[methodIndex].Set();
                        }), new object[] { j, list[j] });

                        if (sleep > 0)
                            Thread.Sleep(sleep);
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
