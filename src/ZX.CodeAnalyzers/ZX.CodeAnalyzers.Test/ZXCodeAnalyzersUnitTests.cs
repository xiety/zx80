using Microsoft.VisualStudio.TestTools.UnitTesting;

using VerifyCS = ZX.CodeAnalyzers.Test.CSharpAnalyzerVerifier<ZX.CodeAnalyzers.ZXCodeAnalyzer>;

namespace ZX.CodeAnalyzers.Test
{
    [TestClass]
    public class ZXCodeAnalyzersUnitTest
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            var test = @"";

            await VerifyCS.VerifyAnalyzerAsync(test);
        }

        [TestMethod]
        public async Task TestMethod2()
        {
            var test = @"using System;
    
namespace ConsoleApplication1
{
    class {|#0:TypeName|}
    {   
        public void Method1()
        {
            byte b = (byte)10;
            Method2({|ZXCodeAnalyzer:b|});
        }

        public void Method2(ushort a)
    {
            }
    }
}";
            await VerifyCS.VerifyAnalyzerAsync(test);
        }
    }
}
