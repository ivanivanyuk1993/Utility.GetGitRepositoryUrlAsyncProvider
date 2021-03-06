using System;
using System.CommandLine.IO;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using GetGitRepositoryUrlAsyncProviderNS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GetGitRepositoryUrlAsyncProviderTestNS;

[TestClass]
public class GetGitRepositoryUrlAsyncProviderTest
{
    [TestMethod]
    public async Task TestMethod1()
    {
        var cancellationToken = CancellationToken.None;
        var systemConsole = new SystemConsole();

        var commandResultOrError = await GetGitRepositoryUrlAsyncProvider.GetGitRepositoryUrlAsync(
            console: systemConsole,
            directoryInfo: new DirectoryInfo(path: AppDomain.CurrentDomain.BaseDirectory),
            cancellationToken: cancellationToken
        );

        commandResultOrError.RunActionWithValueOrError(
            runActionWithValueFunc: gitRepositoryUrl =>
            {
                Assert.AreEqual(
                    actual: gitRepositoryUrl,
                    expected: "git@github.com:ivanivanyuk1993/Utility.GetGitRepositoryUrlAsyncProvider.git"
                );
            },
            runActionWithErrorFunc: exception => throw exception
        );
    }
}