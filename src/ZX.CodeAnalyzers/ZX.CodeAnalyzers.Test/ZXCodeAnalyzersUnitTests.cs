using VerifyCS = Microsoft.CodeAnalysis.CSharp.Testing.CSharpAnalyzerVerifier
    <ZX.CodeAnalyzers.ZXCodeAnalyzer, Microsoft.CodeAnalysis.Testing.DefaultVerifier>;

namespace ZX.CodeAnalyzers.Test;

[TestClass]
public sealed class ZXCodeAnalyzerTests
{
    [TestMethod]
    public async Task TestMethod1() =>
        await VerifyCS.VerifyAnalyzerAsync("");

    [TestMethod]
    public async Task TestMethod2() =>
        await VerifyCS.VerifyAnalyzerAsync(
            """
            using System;

            namespace ConsoleApplication1;

            class {|#0:TypeName|}
            {
                public void Method1()
                {
                    byte b = (byte)10;
                    Method2({|ZXCodeAnalyzer:b|});
                }

                public void Method2(ushort a) { }
            }
            """);
}
