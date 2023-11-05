using System.IO;
using Microsoft.Build.Framework;
using MSBuild.SSH.Utils;
using Renci.SshNet;

namespace MSBuild.SSH;

/// <summary>
/// Uploads provided <see cref="Files"/> to target <see cref="UploadDirectory"/> on the target server.
/// Files are takes as is, no nesting or directory structure is considered, uploads flat to the <see cref="UploadDirectory"/>.
/// </summary>
public class UploadFileTask : SFTPTask
{
	[Required]
	public string[] Files { get; set; }

	[Required]
	public string UploadDirectory { get; set; }

	protected override bool Execute(SftpClient sftp)
	{
		var homePath = sftp.WorkingDirectory;
		LogDebug($"HomePath {homePath}");
		
		foreach (var file in this.Files)
		{
			LogDebug($"Processing file {file}");
			
			var relativeFilePath = Path.GetFileName(file);
			var remotePath =  Path.Combine(this.UploadDirectory, relativeFilePath);
			
			var remoteFile = Path.GetFileName(remotePath);
			var remoteDirectory = PathUtils.CanonizePath(Path.GetDirectoryName(remotePath), homePath);

			sftp.SetPath(remoteDirectory);
			
			LogInfo($"Uploading {remoteFile} to {sftp.WorkingDirectory}");
			using var fileStream = File.OpenRead(file);
			sftp.UploadFile(fileStream, remoteFile);
		}

		return true;
	}
}