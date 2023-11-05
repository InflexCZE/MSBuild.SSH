using System;
using System.Linq;
using Microsoft.Build.Framework;
using Renci.SshNet;

namespace MSBuild.SSH;

/// <summary>
/// Base class for SSH operations.
/// Establishes connection, makes sure we're connect to the right host, closes connection at the end.
/// </summary>
/// <typeparam name="TClient"></typeparam>
public abstract class SSHTask<TClient> : ITask
	where TClient : IBaseClient
{
	public ITaskHost HostObject { get; set; }

	public IBuildEngine BuildEngine { get; set; }

	[Required]
	public string Host { get; set; }
	
	[Required]
	public string Login { get; set; }

	[Required]
	public string Password { get; set; }

	/// <summary>
	/// When set to <see langword="true" />, additional execution info and command results will be logged
	/// </summary>
	public bool LogExecution { get; set; } = true;

	/// <summary>
	/// When provided, only servers within this list will be considered eligible for connection
	/// </summary>
	public string[]? FingerprintWhitelist { get; set; }

	protected abstract bool Execute(TClient client);

	public bool Execute()
	{
		var connection = new ConnectionInfo
		(
			this.Host,
			this.Login,
			new PasswordAuthenticationMethod(this.Login, this.Password)
		);

		var client = (TClient) Activator.CreateInstance(typeof(TClient), connection);
		try
		{
			client.HostKeyReceived += (_, args) =>
			{
				LogDebug($"Received response from {args.FingerPrintMD5} ({args.HostKeyName})");
				
				if (this.FingerprintWhitelist?.Length > 0)
				{
					if (this.FingerprintWhitelist.Contains(args.FingerPrintMD5) == false)
					{
						LogError($"Detected untrusted fingerprint {args.FingerPrintMD5}");
						args.CanTrust = false;
					}
				}
			};

			client.ErrorOccurred += (_, args) =>
			{
				LogError(args.Exception);
			};

			client.Connect();
			LogInfo($"Connected to {this.Host}");

			return Execute(client);
		}
		finally
		{
			LogDebug($"Closing connection to {this.Host}");
			(client as IDisposable)?.Dispose();
		}
	}

	protected void LogError(object message)
	{
		this.BuildEngine.LogErrorEvent
		(
			new
			(
				"",
				this.GetType().Name,
				this.BuildEngine.ProjectFileOfTaskNode,
				this.BuildEngine.LineNumberOfTaskNode,
				this.BuildEngine.ColumnNumberOfTaskNode,
				0,
				0,
				message.ToString(),
				"",
				""
			)
		);
	}

	protected void LogInfo(object message)
	{
		this.BuildEngine.LogMessageEvent
		(
			new
			(
				"",
				this.GetType().Name,
				this.BuildEngine.ProjectFileOfTaskNode,
				this.BuildEngine.LineNumberOfTaskNode,
				this.BuildEngine.ColumnNumberOfTaskNode,
				0,
				0,
				message.ToString(),
				"",
				"",
				this.LogExecution ? MessageImportance.High : MessageImportance.Low
			)
		);
	}
	
	protected void LogDebug(object message)
	{
		this.BuildEngine.LogMessageEvent
		(
			new
			(
				"",
				this.GetType().Name,
				this.BuildEngine.ProjectFileOfTaskNode,
				this.BuildEngine.LineNumberOfTaskNode,
				this.BuildEngine.ColumnNumberOfTaskNode,
				0,
				0,
				message.ToString(),
				"",
				"",
				MessageImportance.Low
			)
		);
	}
}