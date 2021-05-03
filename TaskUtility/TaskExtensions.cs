namespace System.Threading.Tasks
{
	public static class TaskExtensions
	{
		public static async Task ConfigureTimeout(
			this Task task,
			TimeSpan timeout,
			CancellationToken cancellationToken = default)
		{
			using var timeoutCancellation = new CancellationTokenSource();
			using var combinedCancellation = CancellationTokenSource
				.CreateLinkedTokenSource(cancellationToken, timeoutCancellation.Token);

			var delayTask = Task.Delay(timeout, timeoutCancellation.Token);

			var completedTask = await Task.WhenAny(task, delayTask).ConfigureAwait(false);

			timeoutCancellation.Cancel();

			if (completedTask == task)
				await task.ConfigureAwait(false);
			else
				throw new TimeoutException();
		}

		public static async Task<TResult> ConfigureTimeout<TResult>(
			this Task<TResult> task,
			TimeSpan timeout,
			CancellationToken cancellationToken = default)
		{
			using var timeoutCancellation = new CancellationTokenSource();
			using var combinedCancellation = CancellationTokenSource
				.CreateLinkedTokenSource(cancellationToken, timeoutCancellation.Token);

			var delayTask = Task.Delay(timeout, timeoutCancellation.Token);

			var completedTask = await Task.WhenAny(task, delayTask).ConfigureAwait(false);

			timeoutCancellation.Cancel();

			if (completedTask == task)
				return await task.ConfigureAwait(false);
			else
				throw new TimeoutException();
		}
	}
}
