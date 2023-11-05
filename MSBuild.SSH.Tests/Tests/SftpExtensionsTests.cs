using System.Reflection;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using Renci.SshNet.Sftp.Responses;

namespace MSBuilder.SSH.Tests.Tests;

#if false
public class SftpExtensionsTests
{
	private readonly SftpClient Sftp;

	[Fact]
	public void RootPath()
	{
		Assert.False(PathUtils.IsRootedPath("."));
		Assert.True(PathUtils.IsRootedPath("/home/pi"));
		Assert.True(PathUtils.IsRootedPath("/home/pi/"));
	}

	public SftpExtensionsTests()
	{
		this.Sftp = (SftpClient) Activator.CreateInstance
		(
			typeof(SftpClient),
			BindingFlags.CreateInstance | BindingFlags.NonPublic,
			new object[]
			{

			}
		)!;
	}

	class TestSftpSession : ISftpSession
	{
		public bool IsOpen { get; }
		public int OperationTimeout { get; }
		public uint ProtocolVersion { get; }
		public string WorkingDirectory { get; }

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public void Connect()
		{
			throw new NotImplementedException();
		}

		public void Disconnect()
		{
			throw new NotImplementedException();
		}

		public void WaitOnHandle(WaitHandle waitHandle, int millisecondsTimeout)
		{
			throw new NotImplementedException();
		}

		public bool WaitOne(WaitHandle waitHandle, int millisecondsTimeout)
		{
			throw new NotImplementedException();
		}

		public int WaitAny(WaitHandle waitHandleA, WaitHandle waitHandleB, int millisecondsTimeout)
		{
			throw new NotImplementedException();
		}

		public int WaitAny(WaitHandle[] waitHandles, int millisecondsTimeout)
		{
			throw new NotImplementedException();
		}

		public WaitHandle[] CreateWaitHandleArray(params WaitHandle[] waitHandles)
		{
			throw new NotImplementedException();
		}

		public WaitHandle[] CreateWaitHandleArray(WaitHandle waitHandle1, WaitHandle waitHandle2)
		{
			throw new NotImplementedException();
		}

		
		public void ChangeDirectory(string path)
		{
			throw new NotImplementedException();
		}

		public string GetCanonicalPath(string path)
		{
			throw new NotImplementedException();
		}

		public Task<string> GetCanonicalPathAsync(string path, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public SftpFileAttributes RequestFStat(byte[] handle, bool nullOnError)
		{
			throw new NotImplementedException();
		}

		public Task<SftpFileAttributes> RequestFStatAsync(byte[] handle, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public SftpFileAttributes RequestStat(string path, bool nullOnError = false)
		{
			throw new NotImplementedException();
		}

		public SFtpStatAsyncResult BeginStat(string path, AsyncCallback callback, object state)
		{
			throw new NotImplementedException();
		}

		public SftpFileAttributes EndStat(SFtpStatAsyncResult asyncResult)
		{
			throw new NotImplementedException();
		}

		public SftpFileAttributes RequestLStat(string path)
		{
			throw new NotImplementedException();
		}

		public SFtpStatAsyncResult BeginLStat(string path, AsyncCallback callback, object state)
		{
			throw new NotImplementedException();
		}

		public SftpFileAttributes EndLStat(SFtpStatAsyncResult asyncResult)
		{
			throw new NotImplementedException();
		}

		public void RequestMkDir(string path)
		{
			throw new NotImplementedException();
		}

		public byte[] RequestOpen(string path, Flags flags, bool nullOnError = false)
		{
			throw new NotImplementedException();
		}

		public Task<byte[]> RequestOpenAsync(string path, Flags flags, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public SftpOpenAsyncResult BeginOpen(string path, Flags flags, AsyncCallback callback, object state)
		{
			throw new NotImplementedException();
		}

		public byte[] EndOpen(SftpOpenAsyncResult asyncResult)
		{
			throw new NotImplementedException();
		}

		public byte[] RequestOpenDir(string path, bool nullOnError = false)
		{
			throw new NotImplementedException();
		}

		public Task<byte[]> RequestOpenDirAsync(string path, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public void RequestPosixRename(string oldPath, string newPath)
		{
			throw new NotImplementedException();
		}

		public byte[] RequestRead(byte[] handle, ulong offset, uint length)
		{
			throw new NotImplementedException();
		}

		public SftpReadAsyncResult BeginRead(byte[] handle, ulong offset, uint length, AsyncCallback callback, object state)
		{
			throw new NotImplementedException();
		}

		public byte[] EndRead(SftpReadAsyncResult asyncResult)
		{
			throw new NotImplementedException();
		}

		public Task<byte[]> RequestReadAsync(byte[] handle, ulong offset, uint length, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public KeyValuePair<string, SftpFileAttributes>[] RequestReadDir(byte[] handle)
		{
			throw new NotImplementedException();
		}

		public Task<KeyValuePair<string, SftpFileAttributes>[]> RequestReadDirAsync(byte[] handle, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public SftpRealPathAsyncResult BeginRealPath(string path, AsyncCallback callback, object state)
		{
			throw new NotImplementedException();
		}

		public string EndRealPath(SftpRealPathAsyncResult asyncResult)
		{
			throw new NotImplementedException();
		}

		public void RequestRemove(string path)
		{
			throw new NotImplementedException();
		}

		public Task RequestRemoveAsync(string path, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public void RequestRename(string oldPath, string newPath)
		{
			throw new NotImplementedException();
		}

		public Task RequestRenameAsync(string oldPath, string newPath, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public void RequestRmDir(string path)
		{
			throw new NotImplementedException();
		}

		public void RequestSetStat(string path, SftpFileAttributes attributes)
		{
			throw new NotImplementedException();
		}

		public SftpFileSytemInformation RequestStatVfs(string path, bool nullOnError = false)
		{
			throw new NotImplementedException();
		}

		public Task<SftpFileSytemInformation> RequestStatVfsAsync(string path, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public void RequestSymLink(string linkpath, string targetpath)
		{
			throw new NotImplementedException();
		}

		public void RequestFSetStat(byte[] handle, SftpFileAttributes attributes)
		{
			throw new NotImplementedException();
		}

		public void RequestWrite(byte[] handle, ulong serverOffset, byte[] data, int offset, int length, AutoResetEvent wait, Action<SftpStatusResponse> writeCompleted = null)
		{
			throw new NotImplementedException();
		}

		public Task RequestWriteAsync(byte[] handle, ulong serverOffset, byte[] data, int offset, int length, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public void RequestClose(byte[] handle)
		{
			throw new NotImplementedException();
		}

		public Task RequestCloseAsync(byte[] handle, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public SftpCloseAsyncResult BeginClose(byte[] handle, AsyncCallback callback, object state)
		{
			throw new NotImplementedException();
		}

		public void EndClose(SftpCloseAsyncResult asyncResult)
		{
			throw new NotImplementedException();
		}

		public uint CalculateOptimalReadLength(uint bufferSize)
		{
			throw new NotImplementedException();
		}

		public uint CalculateOptimalWriteLength(uint bufferSize, byte[] handle)
		{
			throw new NotImplementedException();
		}

		public ISftpFileReader CreateFileReader(byte[] handle, ISftpSession sftpSession, uint chunkSize, int maxPendingReads, long? fileSize)
		{
			throw new NotImplementedException();
		}
	}
}

#endif