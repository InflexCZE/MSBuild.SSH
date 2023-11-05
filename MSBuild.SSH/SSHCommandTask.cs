using System;
using Microsoft.Build.Framework;
using Renci.SshNet;

namespace MSBuild.SSH;

/// <summary>
/// This task executes bash command on the connected server.
/// Commands can can provided directly in the <see cref="Command"/>,
/// or prepared in *.sh file on server and this file can be invoked instead
/// </summary>
public class SSHCommandTask : SSHTask<SshClient>
{
	[Required]
	public string Command { get; set; }

	/// <summary>
	/// Waiting for the command results will be aborted after this timeout.
	/// Base on <see cref="WaitForCompletion"/>, timeout abortion may or may not be failure.
	/// </summary>
	public int TimeoutSeconds { get; set; } = 10;

	/// <summary>
	/// Should we throw an error when command does not finish in specified timeout <see langword="true" />,
	/// or just silently hang up the session (<see langword="false" />).
	/// The latter one is desirable if we start for example some long running process/service (like a web server).
	/// </summary>
	public bool WaitForCompletion { get; set; } = true;

	protected override bool Execute(SshClient ssh)
	{
		LogDebug($"Executing {this.Command}");
		using var command = ssh.CreateCommand(this.Command);
		
		var executionHandle = command.BeginExecute();

		if (this.TimeoutSeconds < 0)
		{
			executionHandle.AsyncWaitHandle.WaitOne();
		}
		else
		{
			executionHandle.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(this.TimeoutSeconds));
		}

		var error = command.Error;
		if (string.IsNullOrWhiteSpace(error) == false)
		{
			throw new InvalidOperationException(error);
		}

		LogDebug($"Command result {executionHandle.IsCompleted} {command.ExitStatus}");
		LogInfo(command.Result);

		if (executionHandle.IsCompleted == false)
		{
			if (this.WaitForCompletion)
			{
				throw new TimeoutException($"Command {this.Command} did not finish in time");
			}
		}

		return command.ExitStatus == 0;
	}
}