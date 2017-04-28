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
using MyNameSpace;

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
              generator.TypeExpression(SpecialType.System_String), 
              Accessibility.Public,
              getAccessorStatements: new SyntaxNode[] { },
              //{ generator.ReturnStatement(generator.IdentifierName("_lastName")) },
              setAccessorStatements: new SyntaxNode[] { }
              //{ generator.AssignmentStatement(generator.IdentifierName("_lastName"), generator.IdentifierName("value"))}
              );

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
              //generator.TypeExpression(SpecialType.System_TypedReference)),
              ToTypeExpression(Type.GetType("System.String"), true, generator)),
              generator.ParameterDeclaration("FirstName",
              ToTypeExpression(Type.GetType("System.String"), true, generator))};

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
            Employee p = new Employee
            {
                RecursionLevel = 1,
                BusinessEntityID = 2,
                FirstName = "Duy",
                LastName = "Tran",
                ManagerFirstName = "Lindsay",
                ManagerLastName = "Hoff",
                OrganizationNode = "Abc123"
            };
            // (1,1,"Duy","Tran","abc","Lindsay","Hoff");
            Console.WriteLine(p.FirstName + " - " + p.LastName + " - " + p.ManagerFirstName + " - " + p.RecursionLevel);
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
            if(datatypes.ContainsKey(type.FullName))
            {
                return datatypes.FirstOrDefault(t => type.FullName.Equals(t.Key)).Value;
            }
            return SpecialType.None;
        }        
    }
}
