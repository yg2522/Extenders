using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Extensions
{
    public static class Retry
    {
        /// <summary>
        /// Retry with no return value
        /// </summary>
        /// <param name="action">The action that needs to be retried</param>
        /// <param name="retryInterval">How long to wait between each retry</param>
        /// <param name="exceptionCallback">Callback function passing in exception that happened and returns true if you want to retry, false to break out of retry loop</param>
        /// <param name="maxAttemptCount">The number of times to retry</param>
        public static void Do(
            Action action,
            TimeSpan retryInterval,
            Func<Exception, bool> exceptionCallback = null,
            int maxAttemptCount = 3)
        {
            Do<object>(() =>
            {
                action();
                return null;
            }, retryInterval, exceptionCallback, maxAttemptCount);
        }

        /// <summary>
        /// retry with return values
        /// </summary>
        /// <typeparam name="T">The class type that is returned</typeparam>
        /// <param name="action">The action that needs to be retried</param>
        /// <param name="retryInterval">How long to wait between each retry</param>
        /// <param name="exceptionCallback">Callback function passing in exception that happened and returns true if you want to retry, false to break out of retry loop</param>
        /// <param name="maxAttemptCount">The number of times to retry</param>
        /// <returns></returns>
        public static T Do<T>(
            Func<T> action,
            TimeSpan retryInterval,
            Func<Exception, bool> exceptionCallback = null,
            int maxAttemptCount = 3)
        {
            var exceptions = new HashSet<Exception>(new ObjectEqualityComparer<Exception>());
            var retry = true;
            for (int attempted = 0; attempted < maxAttemptCount && retry; attempted++)
            {
                try
                {
                    if (attempted > 0)
                    {
                        Thread.Sleep(retryInterval);
                    }
                    return action();
                }
                catch (Exception ex)
                {
                    retry = exceptionCallback?.Invoke(ex) ?? true;
                    exceptions.Add(ex);
                }
            }
            if (exceptions.Count > 1)
                throw new AggregateException(exceptions);
            else
                throw exceptions.First();
        }
    }
}
