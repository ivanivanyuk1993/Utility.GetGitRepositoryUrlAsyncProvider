using System.CommandLine;
using ExecuteCliCommandAsyncProviderNS;

namespace GetGitRepositoryUrlAsyncProviderNS;

public static class GetGitRepositoryUrlAsyncProvider
{
    public static async Task<string> GetGitRepositoryUrlAsync(
        IConsole console,
        DirectoryInfo directoryInfo,
        CancellationToken cancellationToken
    )
    {
        return
        (
            await ExecuteCliCommandAsyncProvider.ExecuteCliCommandAsync(
                cliCommandText: $"git -C \"{directoryInfo.FullName}\" config --get remote.origin.url",
                console: console,
                cancellationToken: cancellationToken
            )
        ).StandardOutputText;
    }

    public static Task<string> GetGitRepositoryUrlAsync(
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