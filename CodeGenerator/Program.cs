using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.CodeAnalysis.Editing;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using Microsoft.CodeAnalysis.Formatting;
using System.Reflection.Metadata;
using Roslyn.Utilities;
using Microsoft.CodeAnalysis.LanguageServices;
using System.Collections.Generic;
using SF = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using Microsoft.CodeAnalysis.Options;
//using Roslyn.Compilers;
//using Roslyn.Compilers.CSharp;

namespace CodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {

            //update from here
            CompilationUnitSyntax cu = SF.CompilationUnit();
            cu = SF.CompilationUnit()
            .AddUsings(SF.UsingDirective(SF.IdentifierName("System")))
            .AddUsings(SF.UsingDirective(SF.IdentifierName("System.Generic")))
            ;

            ClassDeclarationSyntax c = SF.ClassDeclaration("MyClass")
            .AddModifiers(SF.Token(SyntaxKind.PublicKeyword))
            .AddModifiers(SF.Token(SyntaxKind.PartialKeyword))
            ;
            // Add a property
            PropertyDeclarationSyntax @property =
                SF.PropertyDeclaration(
            SF.NullableType(
                SF.PredefinedType(
                    SF.Token(SyntaxKind.IntKeyword))),
            SF.Identifier("MyProperty"))
                .AddModifiers(SF.Token(SyntaxKind.PublicKeyword));
            // Add a getter
            @property = @property.AddAccessorListAccessors(
                SF.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                    .WithSemicolonToken(SF.Token(SyntaxKind.SemicolonToken)
                    ));
            // Add a private setter
            @property = @property.AddAccessorListAccessors(
                SF.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                //.AddModifiers(SF.Token(SyntaxKind.PrivateKeyword))
                .WithSemicolonToken(SF.Token(SyntaxKind.SemicolonToken)
                ));
            // Add the property to the class
            c = c.AddMembers(@property);
            NamespaceDeclarationSyntax ns = SF.NamespaceDeclaration(SF.IdentifierName("MyNamespace"));
            ns = ns.AddMembers(c);
            cu = cu.AddMembers(ns);
            AdhocWorkspace cw = new AdhocWorkspace();
            OptionSet options = cw.Options;
            //options = options.WithChangedOption(CSharpFormattingOptions.NewLinesForBracesInProperties, true);
            //options = options.WithChangedOption(CSharpFormattingOptions.NewLinesForBracesInTypes, true);
            SyntaxNode formattedNode = Formatter.Format(cu, cw, options);
            StringBuilder sbu = new StringBuilder();
            using (StringWriter writer = new StringWriter(sbu))
            {
                formattedNode.WriteTo(writer);
                Console.WriteLine(writer);
            }
            Console.ReadLine();

        }

        private static SyntaxNode ToTypeExpression(Type type, bool nullable, SyntaxGenerator generator)
        {
            SyntaxNode baseType;
            SyntaxNode propType;

            SpecialType specialType = ToSpecialType(type);

            if (specialType == SpecialType.None)
            {
                baseType = generator.IdentifierName(type.Name);
            }
            else
            {
                baseType = generator.TypeExpression(specialType);
            }

            if (nullable && type.IsValueType)
            {
                propType = generator.NullableTypeExpression(baseType);
            }
            else
            {
                propType = baseType;
            }

            return propType;

        }
        private static SpecialType ToSpecialType(Type type)
        {
            Dictionary<string, SpecialType> datatypes = new Dictionary<string, SpecialType>();
            Dictionary<string, string> datatypes1 = new Dictionary<string, string>();
            datatypes.Add("System.Boolean", SpecialType.System_Boolean);
            datatypes.Add("System.Byte", SpecialType.System_Byte);
            datatypes.Add("System.SByte", SpecialType.System_SByte);
            datatypes.Add("System.Char", SpecialType.System_Char);
            datatypes.Add("System.Decimal", SpecialType.System_Decimal);
            datatypes.Add("System.Double", SpecialType.System_Double);
            datatypes.Add("System.Single", SpecialType.System_Single);
            datatypes.Add("System.Int32", SpecialType.System_Int32);
            datatypes.Add("System.UInt32", SpecialType.System_UInt32);
            datatypes.Add("System.Int64", SpecialType.System_Int64);
            datatypes.Add("System.UInt64", SpecialType.System_UInt64);
            datatypes.Add("System.Object", SpecialType.System_Object);
            datatypes.Add("System.Int16", SpecialType.System_Int16);
            datatypes.Add("System.UInt16", SpecialType.System_UInt16);
            datatypes.Add("System.String", SpecialType.System_String);
            datatypes.Add("System.Void", SpecialType.System_Void);
            if (datatypes.ContainsKey(type.FullName))
            {
                return datatypes.FirstOrDefault(t => type.FullName.Equals(t.Key)).Value;
            }
            return SpecialType.None;
        }
    }
}
