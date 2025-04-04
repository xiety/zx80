﻿using System.Globalization;
using System.Text;

namespace ZX.CodeGeneration;

public class OpcodesGenerator(Item[] items, string outputPattern)
{
    public void Run(string prefix, bool argumentBeforeOperand = false)
    {
        var outputFilename = String.Format(outputPattern, prefix);

        var sb = new StringBuilder();

        sb.AppendLine($$"""
            //This file is autogenerated, do not change it
            #pragma warning disable CS0168 // Variable is declared but never used
            #pragma warning disable CS0162 // Unreachable code detected
            namespace ZX.Core.Cpu;

            public partial class CpuRuntime
            {
                private (int, string) Interpret{{prefix}}()
                {
                    bool isFast; //cannot be declared inside multiple switch cases
            """);

        if (argumentBeforeOperand)
            sb.AppendLine("        var arg = ReadByteOpcode();");

        sb.AppendLine($$"""
                var op = ReadByteOpcode();

                switch (op)
                {
        """);

        var index = 0;

        foreach (var item in items.Where(a => a.Prefix == prefix))
        {
            var op = ParseHex(argumentBeforeOperand
                ? item.Code[(prefix.Length + 2)..(prefix.Length + 4)]
                : item.Code[prefix.Length..(prefix.Length + 2)]);

            var desc = item.Name;

            sb.AppendLine($$"""
                        case 0x{{op:X2}}:
            """);

            if (desc.StartsWith("Lookup "))
            {
                var anotherTable = desc["Lookup ".Length..];

                sb.AppendLine($$"""
                                return Interpret{{anotherTable}}();
                """);
            }
            else
            {
                var (originalMethod, originalArguments) = Convert(desc);

                var method = ConvertMethod(originalMethod);
                var arguments = ConvertArguments(originalArguments, originalMethod, argumentBeforeOperand);

                var opcodes = argumentBeforeOperand ? $"{prefix}XX{op:X2}" : $"{prefix}{op:X2}";

                if (item.TimeFast != 0)
                {
                    sb.AppendLine($$"""
                                    isFast = {{method}}({{String.Join(", ", arguments)}});
                                    return (isFast ? {{item.TimeFast}} : {{item.TimeSlow}}, "{{desc}}");
                    """);
                }
                else
                {
                    sb.AppendLine($$"""
                                    {{method}}({{String.Join(", ", arguments)}});
                                    return ({{item.TimeSlow}}, "{{desc}}");
                    """);
                }
            }

            index++;
        }

        sb.AppendLine($$"""
                    default:
                        //throw new Exception($"\{op:X2\}");
                        Console.WriteLine($"Unknown operation: {{prefix}} {op:X2}");
                        return (0, "UNKNOWN");
                }
            }
        }
        """);

        File.WriteAllText(outputFilename, sb.ToString());
    }

    private static (string Method, string[] Arguments) Convert(string text)
    {
        var n = text.IndexOf(' ');

        if (n > 0)
        {
            var method = text[..n];
            var args = text[(n + 1)..].Trim().Split(",");

            return (method, args);
        }

        return (text, Array.Empty<string>());
    }

    private static string ConvertMethod(string text)
        => CapitalLetter(text);

    private static string[] ConvertArguments(string[] sep, string operation, bool argumentBeforeOperand)
    {
        var bits = GuessBitness(operation, sep);

        var converted = sep.Select((a, i) => ConvertArg(operation, sep.Length, a, i, bits, argumentBeforeOperand))
                           .Where(a => a != null)
                           .ToArray();

        return converted!;
    }

    private static readonly string[] args16bit = ["hl", "ix", "iy", "bc", "af", "af'", "de", "sp"];

    private static int GuessBitness(string operation, string[] sep)
        => operation is "jp" || args16bit.Any(a => sep.Contains(a)) ? 16 : 8;

    private static string? ConvertArg(string operation, int total, string arg, int index, int bits, bool argumentBeforeOperand)
    {
        if (operation is "djnz" or "jr" && arg is "d")
            return null;

        if (operation is "jp" or "call" && arg is "nn")
            return null;

        if (operation is "jp" or "jr" or "ret" or "call" && arg is "c")
            return "flag.C == true";

        var isRef = operation switch
        {
            "rrc" or "sla" or "sra" or "sll" or "rl" or "rlc" or "rrc" or "rr"
                => index == total - 1,
            "set" or "res" or "srl" => true,
            "ex" => true,
            "sub" or "and" or "xor" or "or" or "cp" or "push" or "tst" or "in" when total is 1
                => false,
            "jp" or "out" => false,
            _ => index == 0,
        };

        var prefix = isRef ? "ref " : "";
        var postfix = isRef ? "Ref" : "";
        var memoryRef = (bits == 8 ? "GetByteMemory" : "GetUShortMemory") + postfix;

        var ret = arg switch
        {
            "n" => "ReadByteOpcode()",
            "nn" => "ReadUShortOpcode()",
            "(n)" => "ReadByteOpcode()", //port in/out
            "(c)" => null, //port in/out
            "(nn)" => prefix + $"{memoryRef}(ReadUShortOpcode())",
            "a" => prefix + "reg.A",
            "af" => prefix + "reg.AF",
            "af'" => prefix + "reg.AFs",
            "b" => prefix + "reg.B",
            "c" => prefix + "reg.C",
            "d" => prefix + "reg.D",
            "e" => prefix + "reg.E",
            "h" => prefix + "reg.H",
            "l" => prefix + "reg.L",
            "i" => prefix + "reg.I",
            "r" => prefix + "reg.R",
            "ixh" => prefix + "reg.IXH",
            "ixl" => prefix + "reg.IXL",
            "iyh" => prefix + "reg.IYH",
            "iyl" => prefix + "reg.IYL",
            "bc" => prefix + "reg.BC",
            "de" => prefix + "reg.DE",
            "hl" => prefix + "reg.HL",
            "sp" => prefix + "reg.SP",
            "ix" => prefix + "reg.IX",
            "iy" => prefix + "reg.IY",
            "(bc)" => prefix + $"{memoryRef}(reg.BC)",
            "(de)" => prefix + $"{memoryRef}(reg.DE)",
            "(hl)" => prefix + $"{memoryRef}(reg.HL)",
            "(sp)" => prefix + $"{memoryRef}(reg.SP)",
            "(ix)" => prefix + "reg.IX", //TODO: Loads the value of IX into PC
            "(ix+d)" when !argumentBeforeOperand => prefix + $"{memoryRef}(reg.IX, ReadByteOpcode())",
            "(ix+d)" when argumentBeforeOperand => prefix + $"{memoryRef}(reg.IX, arg)",
            "(iy)" => prefix + "reg.IY", //TODO: Loads the value of IY into PC
            "(iy+d)" when !argumentBeforeOperand => prefix + $"{memoryRef}(reg.IY, ReadByteOpcode())",
            "(iy+d)" when argumentBeforeOperand => prefix + $"{memoryRef}(reg.IY, arg)",
            "z" => "flag.Z == true",
            "nz" => "flag.Z == false",
            "fc" => "flag.C == true",
            "nc" => "flag.C == false",
            "pe" => "flag.P == true",
            "po" => "flag.P == false",
            "m" => "flag.S == true",
            "p" => "flag.S == false",
            "0" or "1" or "2" or "3" or "4" or "5" or "6" or "7" or "8" or "9" => arg,
            "00h" or "08h" or "10h" or "18h" or "20h" or "28h" or "30h" or "38h" => "0x" + arg[..^1],
            _ => throw new Exception($"Unknown argument {arg}"),
        };

        return ret;
    }

    private static int ParseHex(string text)
        => Int32.Parse(text, NumberStyles.HexNumber);

    private static string CapitalLetter(string text)
        => Char.ToUpper(text[0]) + text[1..];
}

public record Item(string Prefix, string Name, int Size, int TimeSlow, int TimeFast, string Code);
