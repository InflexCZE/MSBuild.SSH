using Renci.SshNet;

namespace MSBuild.SSH;

/// <summary>
/// Common base for SFTP operations
/// </summary>
public abstract class SFTPTask : SSHTask<SftpClient>
{ }