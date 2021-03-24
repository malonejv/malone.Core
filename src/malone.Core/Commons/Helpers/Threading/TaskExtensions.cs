//<author>Javier López Malone</author>
//<date>25/11/2020 02:48:01</date>

using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace malone.Core.Commons.Helpers.Threading
{
    /// <summary>
    /// Defines the <see cref="TaskExtensions" />.
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        /// The WithCurrentCulture.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="task">The task<see cref="Task{T}"/>.</param>
        /// <returns>The <see cref="CultureAwaiter{T}"/>.</returns>
        public static CultureAwaiter<T> WithCurrentCulture<T>(this Task<T> task)
        {
            return new CultureAwaiter<T>(task);
        }

        /// <summary>
        /// The WithCurrentCulture.
        /// </summary>
        /// <param name="task">The task<see cref="Task"/>.</param>
        /// <returns>The <see cref="CultureAwaiter"/>.</returns>
        public static CultureAwaiter WithCurrentCulture(this Task task)
        {
            return new CultureAwaiter(task);
        }

        /// <summary>
        /// Defines the <see cref="CultureAwaiter{T}" />.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        public struct CultureAwaiter<T> : ICriticalNotifyCompletion
        {
            /// <summary>
            /// Defines the _task.
            /// </summary>
            private readonly Task<T> _task;

            /// <summary>
            /// Initializes a new instance of the <see cref=""/> class.
            /// </summary>
            /// <param name="task">The task<see cref="Task{T}"/>.</param>
            public CultureAwaiter(Task<T> task)
            {
                _task = task;
            }

            /// <summary>
            /// The GetAwaiter.
            /// </summary>
            /// <returns>The <see cref="CultureAwaiter{T}"/>.</returns>
            public CultureAwaiter<T> GetAwaiter()
            {
                return this;
            }

            /// <summary>
            /// Gets a value indicating whether IsCompleted.
            /// </summary>
            public bool IsCompleted
            {
                get { return _task.IsCompleted; }
            }

            /// <summary>
            /// The GetResult.
            /// </summary>
            /// <returns>The <see cref="T"/>.</returns>
            public T GetResult()
            {
                return _task.GetAwaiter().GetResult();
            }

            /// <summary>
            /// The OnCompleted.
            /// </summary>
            /// <param name="continuation">The continuation<see cref="Action"/>.</param>
            public void OnCompleted(Action continuation)
            {
                // The compiler will never call this method
                throw new NotImplementedException();
            }

            /// <summary>
            /// The UnsafeOnCompleted.
            /// </summary>
            /// <param name="continuation">The continuation<see cref="Action"/>.</param>
            public void UnsafeOnCompleted(Action continuation)
            {
                var currentCulture = Thread.CurrentThread.CurrentCulture;
                var currentUiCulture = Thread.CurrentThread.CurrentUICulture;
                _task.ConfigureAwait(false).GetAwaiter().UnsafeOnCompleted(() =>
                {
                    var originalCulture = Thread.CurrentThread.CurrentCulture;
                    var originalUiCulture = Thread.CurrentThread.CurrentUICulture;
                    Thread.CurrentThread.CurrentCulture = currentCulture;
                    Thread.CurrentThread.CurrentUICulture = currentUiCulture;
                    try
                    {
                        continuation();
                    }
                    finally
                    {
                        Thread.CurrentThread.CurrentCulture = originalCulture;
                        Thread.CurrentThread.CurrentUICulture = originalUiCulture;
                    }
                });
            }
        }

        /// <summary>
        /// Defines the <see cref="CultureAwaiter" />.
        /// </summary>
        public struct CultureAwaiter : ICriticalNotifyCompletion
        {
            /// <summary>
            /// Defines the _task.
            /// </summary>
            private readonly Task _task;

            /// <summary>
            /// Initializes a new instance of the <see cref=""/> class.
            /// </summary>
            /// <param name="task">The task<see cref="Task"/>.</param>
            public CultureAwaiter(Task task)
            {
                _task = task;
            }

            /// <summary>
            /// The GetAwaiter.
            /// </summary>
            /// <returns>The <see cref="CultureAwaiter"/>.</returns>
            public CultureAwaiter GetAwaiter()
            {
                return this;
            }

            /// <summary>
            /// Gets a value indicating whether IsCompleted.
            /// </summary>
            public bool IsCompleted
            {
                get { return _task.IsCompleted; }
            }

            /// <summary>
            /// The GetResult.
            /// </summary>
            public void GetResult()
            {
                _task.GetAwaiter().GetResult();
            }

            /// <summary>
            /// The OnCompleted.
            /// </summary>
            /// <param name="continuation">The continuation<see cref="Action"/>.</param>
            public void OnCompleted(Action continuation)
            {
                // The compiler will never call this method
                throw new NotImplementedException();
            }

            /// <summary>
            /// The UnsafeOnCompleted.
            /// </summary>
            /// <param name="continuation">The continuation<see cref="Action"/>.</param>
            public void UnsafeOnCompleted(Action continuation)
            {
                var currentCulture = Thread.CurrentThread.CurrentCulture;
                var currentUiCulture = Thread.CurrentThread.CurrentUICulture;
                _task.ConfigureAwait(false).GetAwaiter().UnsafeOnCompleted(() =>
                {
                    var originalCulture = Thread.CurrentThread.CurrentCulture;
                    var originalUiCulture = Thread.CurrentThread.CurrentUICulture;
                    Thread.CurrentThread.CurrentCulture = currentCulture;
                    Thread.CurrentThread.CurrentUICulture = currentUiCulture;
                    try
                    {
                        continuation();
                    }
                    finally
                    {
                        Thread.CurrentThread.CurrentCulture = originalCulture;
                        Thread.CurrentThread.CurrentUICulture = originalUiCulture;
                    }
                });
            }
        }
    }
}
