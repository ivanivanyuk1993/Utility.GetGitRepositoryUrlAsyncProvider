using System.CommandLine;
using CliExitCodeProviderNS;
using ExecuteCliCommandAsyncProviderNS;
using ValueOrErrorNS;

namespace GetGitRepositoryUrlAsyncProviderNS;

public static class GetGitRepositoryUrlAsyncProvider
{
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
            ? ValueOrError<string, Exception>.Value(
                value: executeCliCommandResult.StandardOutputTextList.First()!
            )
            : ValueOrError<string, Exception>.Error(
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