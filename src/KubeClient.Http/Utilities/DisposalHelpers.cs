using System;
using System.Collections.Generic;
using System.Linq;

namespace KubeClient.Http.Utilities
{
    /// <summary>
    ///		Helper methods for <see cref="IDisposable"/>.
    /// </summary>
    static class DisposalHelpers
    {
        /// <summary>
        ///		Create an aggregate <see cref="IDisposable"/> that disposes of the specified <see cref="IDisposable"/>s when it is disposed.
        /// </summary>
        /// <param name="disposables">
        ///		The <see cref="IDisposable"/>s to aggregate.
        /// </param>
        /// <returns>
        ///		An aggregate <see cref="IDisposable"/> representing the supplied disposables.
        /// </returns>
        /// <exception cref="AggregateException">
        ///		One or more aggregated disposables throw exceptions during disposal.
        /// </exception>
        public static AggregateDisposable ToAggregateDisposable(this IEnumerable<IDisposable> disposables)
        {
            if (disposables == null)
                return new AggregateDisposable();

            return new AggregateDisposable(disposables);
        }

        #region AggregateDisposable

        /// <summary>
        ///		Implements disposal of multiple <see cref="IDisposable"/>s.
        /// </summary>
        public struct AggregateDisposable
            : IDisposable
        {
            /// <summary>
            ///		The disposables to dispose of.
            /// </summary>
            readonly IReadOnlyList<IDisposable> _disposables;

            /// <summary>
            ///		Create a new aggregate disposable.
            /// </summary>
            /// <param name="disposables">
            ///		A sequence of <see cref="IDisposable"/>s to aggregate.
            /// </param>
            public AggregateDisposable(IEnumerable<IDisposable> disposables)
            {
                if (disposables == null)
                    throw new ArgumentNullException(nameof(disposables));

                _disposables = disposables.ToArray();
            }

            /// <summary>
            ///		Dispose the disposables.
            /// </summary>
            public void Dispose()
            {
                List<Exception> disposalExceptions = new List<Exception>();
                foreach (IDisposable disposable in _disposables)
                {
                    try
                    {
                        disposable.Dispose();
                    }
                    catch (Exception eDisposal)
                    {
                        disposalExceptions.Add(eDisposal);
                    }
                }

                if (disposalExceptions.Count > 0)
                    throw new AggregateException("One or more exceptions were encountered during object disposal.", disposalExceptions);
            }
        }

        #endregion // AggregateDisposable
    }
}