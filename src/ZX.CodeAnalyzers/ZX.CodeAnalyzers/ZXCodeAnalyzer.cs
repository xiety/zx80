using System.Collections.Immutable;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace ZX.CodeAnalyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class ZXCodeAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "ZXCodeAnalyzer";

        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.AnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.AnalyzerDescription), Resources.ResourceManager, typeof(Resources));
        private const string Category = "Naming";

        private static readonly DiagnosticDescriptor Rule
            = new(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Error, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();

            context.RegisterOperationAction(AnalyzeOperationAction, OperationKind.Conversion);
        }

        private void AnalyzeOperationAction(OperationAnalysisContext context)
        {
            if (context.Operation is null || !context.Operation.IsImplicit)
                return;

            if (context.Operation is not Microsoft.CodeAnalysis.Operations.IConversionOperation op)
                return;

            if (op.Operand is null || op.Operand.Type is null || op.Type is null)
                return;

            var nameFrom = op.Operand.Type.Name;
            var nameTo = op.Type.Name;

            if (nameFrom != "Byte" || nameTo != "UInt16")
                return;

            if (op.Syntax is null)
                return;

            context.ReportDiagnostic(Diagnostic.Create(Rule, context.Operation.Syntax.GetLocation(),
                 nameFrom, nameTo));
        }
    }
}
