using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.IO;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Editing;
using System.Text;

namespace CodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get a workspace
            var workspace = new AdhocWorkspace();

            // Get the SyntaxGenerator for the specified language
            var generator = SyntaxGenerator.GetGenerator(workspace, LanguageNames.CSharp);

            // Create using/Imports directives
            var usingDirectives = generator.NamespaceImportDeclaration("System");

            // Generate two private fields
            var lastNameField = generator.FieldDeclaration("_lastName",
              generator.TypeExpression(SpecialType.System_String),
              Accessibility.Private);
            var firstNameField = generator.FieldDeclaration("_firstName",
              generator.TypeExpression(SpecialType.System_String),
              Accessibility.Private);

            // Generate two properties with explicit get/set
            var lastNameProperty = generator.PropertyDeclaration("LastName",
              generator.TypeExpression(SpecialType.System_String), Accessibility.Public,
              getAccessorStatements: new SyntaxNode[]
              { generator.ReturnStatement(generator.IdentifierName("_lastName")) },
              setAccessorStatements: new SyntaxNode[]
              { generator.AssignmentStatement(generator.IdentifierName("_lastName"),
                generator.IdentifierName("value"))});
            var firstNameProperty = generator.PropertyDeclaration("FirstName",
              generator.TypeExpression(SpecialType.System_String),
              Accessibility.Public,
              getAccessorStatements: new SyntaxNode[]
              { generator.ReturnStatement(generator.IdentifierName("_firstName")) },
              setAccessorStatements: new SyntaxNode[]
              { generator.AssignmentStatement(generator.IdentifierName("_firstName"),
                generator.IdentifierName("value")) });

            // Generate parameters for the class' constructor
            var constructorParameters = new SyntaxNode[] {
              generator.ParameterDeclaration("LastName",
              //generator.NullableTypeExpression(lastNameProperty)),
              generator.TypeExpression(SpecialType.System_String)),
              generator.ParameterDeclaration("FirstName",
              generator.TypeExpression(SpecialType.System_String)) };

            // Generate the constructor's method body
            var constructorBody = new SyntaxNode[] {
              generator.AssignmentStatement(generator.IdentifierName("_lastName"),
              generator.IdentifierName("LastName")),
              generator.AssignmentStatement(generator.IdentifierName("_firstName"),
              generator.IdentifierName("FirstName"))};


            // Generate the class' constructor
            var constructor = generator.ConstructorDeclaration("Person",
              constructorParameters, Accessibility.Public,
              statements: constructorBody);

            // An array of SyntaxNode as the class members
            var members = new SyntaxNode[] { lastNameField,
                firstNameField, lastNameProperty, firstNameProperty,
                constructor };

            //add class base
            var baseNode = generator.IdentifierName("PersonBase");
            // Generate the class
            var classDefinition = generator.ClassDeclaration(
              "Person", typeParameters: null,
              accessibility: Accessibility.Public,
              modifiers: DeclarationModifiers.Partial,
              baseType: baseNode, //change baseNote to null if you want to remove inherance from PersonBase
              members: members);


            // Declare a namespace
            var namespaceDeclaration = generator.NamespaceDeclaration("MyTypes", classDefinition);

            // Get a CompilationUnit (code file) for the generated code
            var newNode = generator.CompilationUnit(usingDirectives, namespaceDeclaration).
              NormalizeWhitespace();


            StringBuilder sb = new StringBuilder();
            using (StringWriter writer = new StringWriter(sb))
            {
                newNode.WriteTo(writer);
                Console.Write(writer.ToString());
            };
            Console.ReadLine();
        }
    }
}
