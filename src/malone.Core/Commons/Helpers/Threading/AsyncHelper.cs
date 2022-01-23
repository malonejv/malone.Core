//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:00</date>

using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace malone.Core.Commons.Helpers.Threading
{
    /// <summary>
    /// Defines the <see cref="AsyncHelper" />.
    /// </summary>
    public static class AsyncHelper
    {
        /// <summary>
        /// Defines the _myTaskFactory.
        /// </summary>
        private static readonly TaskFactory _myTaskFactory = new TaskFactory(CancellationToken.None,
            TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);

        /// <summary>
        /// The RunSync.
        /// </summary>
        /// <typeparam name="TResult">.</typeparam>
        /// <param name="func">The func<see cref="Func{Task{TResult}}"/>.</param>
        /// <returns>The <see cref="TResult"/>.</returns>
        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            var cultureUi = CultureInfo.CurrentUICulture;
            var culture = CultureInfo.CurrentCulture;
            return _myTaskFactory.StartNew(() =>
            {
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = cultureUi;
                return func();
            }).Unwrap().GetAwaiter().GetResult();
        }

        /// <summary>
        /// The RunSync.
        /// </summary>
        /// <param name="func">The func<see cref="Func{Task}"/>.</param>
        public static void RunSync(Func<Task> func)
        {
            var cultureUi = CultureInfo.CurrentUICulture;
            var culture = CultureInfo.CurrentCulture;
            _myTaskFactory.StartNew(() =>
            {
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = cultureUi;
                return func();
            }).Unwrap().GetAwaiter().GetResult();
        }
    }
}
