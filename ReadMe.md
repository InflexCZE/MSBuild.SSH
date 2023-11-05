# MSBuild.SSH
Ever wanted to automatically deploy to a SFTP server or execute SSH command as part of your build to trigger an automatic update script, but couldn't cos vanilla MSBuild doesn't support that.
Cry no more as this little lib got you covered!

[![Version](https://img.shields.io/nuget/vpre/MSBuild.SSH.svg)](https://www.nuget.org/packages/MSBuild.SSH)
[![NuGet download count](https://img.shields.io/nuget/dt/MSBuild.SSH.svg)](https://www.nuget.org/packages/MSBuild.SSH)

## How to use
Just add latest [MSBuild.SSH](https://www.nuget.org/packages/MSBuild.SSH) to your `csproj` or `pubxml` and rock on!
```xml
<PackageReference Include="MSBuild.SSH" Version="1.0.0.0" PrivateAssets="all" />
```

## Available tasks
### UploadFileTask
This task uploads specified file(s) to the target directory on the server.
The files are takes as is, no nesting or directory structure is considered, uploads flat to the UploadDirectory.

```xml
<UploadFileTask
    Host="$(DEPLOY_SERVER)"
    Login="$(SECRET_USER_NAME)"
    Password="$(SECRET_PASSWORD)"
    Files="$(publishUrl)\$(MSBuildProjectName).zip"
    UploadDirectory="DeployTargetFolder"
    />
```

### SSHCommandTask
This task executes bash command on the connected server.
Series of commands can can provided directly in the `Command` parameter, or prepared in an `*.sh` file on the server and invoked as shown in the example bellow
```xml
<SSHCommandTask
    Host="$(DEPLOY_SERVER)"
    Login="$(SECRET_USER_NAME)"
    Password="$(SECRET_PASSWORD)"
    Command="./Update.sh"
    />
```

## Security
As the name suggest, [SSH](https://en.wikipedia.org/wiki/Secure_Shell) (Secure Shell) is by default designed to be secure by encryption. To make sure you're connecting to the correct server, not an impostor, all tasks can be optionally supplied with list of `FingerprintWhitelist`. Any connection to a server outside of this list will be immediately rejected.
```xml
<SSHCommandTask
    Host="$(DEPLOY_SERVER)"
    Login="$(SECRET_USER_NAME)"
    Password="$(SECRET_PASSWORD)"

    <!-- 12:f8:7e:78:61:b4:bf:e2:de:24:15:96:4e:d4:72:53 -->
    FingerprintWhitelist="$(DEPLOY_SERVER_FINGERPRINT)"

    Command="echo pong"
    />
```

## Debugging
All tasks automatically log excecution progress to MSBuild output.
By default, only important progress info and errors are printed, but additional details be be obtained by increasing logging verbosity

https://learn.microsoft.com/en-us/visualstudio/msbuild/msbuild-command-line-reference#switches
https://stackoverflow.com/questions/1211841/how-can-i-make-visual-studios-build-be-very-verbose

On the contrary, should you not enjoy default logging of the tasks, `LogExecution="False"` can be used in all tasks. In this case the logs will be limited only to errors.


## Contribution
As usual, contributions are welcome. Leave a PR and I'll surely merge it.
Please follow coding style, test properly before submit, and explain what the new feature brings in, ideally the docs form.
