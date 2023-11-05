using System.Collections.Generic;
using System.Linq;
using Renci.SshNet;
using Renci.SshNet.Common;

namespace MSBuild.SSH.Utils;

public static class SftpExtensions
{
    public static void CreateDirectoriesIfNeeded(this SftpClient sftp, string path)
    {
        if (sftp.Exists(path))
        {
            // Fast path, typically the directory structure already exists
            return;
        }

        var requiredSegments = new Stack<string>();
        foreach (var pathSegment in PathUtils.IncrementalPathSegments(path).Reverse())
        {
            if (sftp.Exists(pathSegment))
                break;

            requiredSegments.Push(pathSegment);
        }

        foreach (var pathSegment in requiredSegments)
        {
            sftp.CreateDirectory(pathSegment);
        }
    }

    public static void SetPath(this SftpClient sftp, string path, bool createDirectoriesIfNeeded = true)
    {
        path = PathUtils.CanonizePath(path, sftp.WorkingDirectory);

        if (sftp.WorkingDirectory != path)
        {
        Retry:
            try
            {
                // First always try. There can be links of what not,
                // test has no cost, especially if we typically expected success
                sftp.ChangeDirectory(path);
                return;
            }
            catch (SftpPathNotFoundException) when (createDirectoriesIfNeeded)
            { }

            sftp.CreateDirectoriesIfNeeded(path);
            createDirectoriesIfNeeded = false;
            goto Retry;

        }
    }
}