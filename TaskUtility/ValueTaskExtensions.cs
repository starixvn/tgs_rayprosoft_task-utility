namespace System.Threading.Tasks
{
	public static class ValueTaskExtensions
	{
		public static async ValueTask ConfigreTimeout(
			this ValueTask task,
			TimeSpan timeout,
			CancellationToken cancellationToken = default)
			=> await task.AsTask()
				.ConfigureTimeout(timeout, cancellationToken)
				.ConfigureAwait(false);

		public static async ValueTask<TResult> ConfigureTimeout<TResult>(
			this ValueTask<TResult> task,
			TimeSpan timeout,
			CancellationToken cancellationToken = default)
			=> await task.AsTask()
				.ConfigureTimeout(timeout, cancellationToken)
				.ConfigureAwait(false);
	}
}
