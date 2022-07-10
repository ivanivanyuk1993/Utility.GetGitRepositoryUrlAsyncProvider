using System;
using System.CommandLine;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CliExitCodeProviderNS;
using ExecuteCliCommandAsyncProviderNS;
using ValueOrErrorNS;

namespace GetGitRepositoryUrlAsyncProviderNS;

public static class GetGitRepositoryUrlAsyncProvider
{
    /// <summary>
    ///     todo replace <see cref="Task"/>-s with <see cref="IObservable{T}"/>-s
    /// </summary>
    /// <param name="console"></param>
    /// <param name="directoryInfo"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<ValueOrError<string, Exception>> GetGitRepositoryUrlAsync(
        IConsole console,
        DirectoryInfo directoryInfo,
        CancellationToken cancellationToken
    )
    {
        var executeCliCommandResult = await ExecuteCliCommandAsyncProvider.ExecuteCliCommandAsync(
            cliCommandText: $"git -C \"{directoryInfo.FullName}\" config --get remote.origin.url",
            console: console,
            cancellationToken: cancellationToken
        );
        return executeCliCommandResult.ExitCode.IsSuccessfulCliExitCode()
            ? ValueOrError<string, Exception>.CreateValue(
                value: executeCliCommandResult.StandardOutputTextList.First()!
            )
            : ValueOrError<string, Exception>.CreateError(
                error: new Exception(
                    message: executeCliCommandResult.StandardErrorOutputText
                )
            );
    }

    public static Task<ValueOrError<string, Exception>> GetGitRepositoryUrlAsync(
        IConsole console,
        FileInfo fileInfo,
        CancellationToken cancellationToken
    )
    {
        return GetGitRepositoryUrlAsync(
            console: console,
            directoryInfo: fileInfo.Directory!,
            cancellationToken: cancellationToken
        );
    }
}
