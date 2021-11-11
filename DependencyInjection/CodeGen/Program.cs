using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.Text;

namespace SamHowes.Extensions.DependencyInjection.CodeGen
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var generator = new Generator(new Editor(), "SamHowes.Extensions.DependencyInjection.IO",
                Directory.GetCurrentDirectory());
            foreach (var type in new[] {typeof(File)})
            {
                await generator.Generate(type);
            }
        }
    }


    public class Generator
    {
        private readonly Editor _editor;
        private readonly string _rootNamespace;
        private readonly string _outputDirectory;

        // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/built-in-types
        public static Dictionary<string, string> TypeAliases = new()
        {
            ["System.Boolean"] = "bool",
            ["System.Byte"] = "byte",
            ["System.SByte"] = "sbyte",
            ["System.Char"] = "char",
            ["System.Decimal"] = "decimal",
            ["System.Double"] = "double",
            ["System.Single"] = "float",
            ["System.Int32"] = "int",
            ["System.UInt32"] = "uint",
            ["System.IntPtr"] = "nint",
            ["System.UIntPtr"] = "nuint",
            ["System.Int64"] = "long",
            ["System.UInt64"] = "ulong",
            ["System.Int16"] = "short",
            ["System.UInt16"] = "ushort",
            ["System.Object"] = "object",
            ["System.String"] = "string",
            ["System.Object"] = "dynamic",
            ["System.Void"] = "void",
        };

        public Generator(Editor editor, string rootNamespace, string outputDirectory)
        {
            _editor = editor;
            _rootNamespace = rootNamespace;
            _outputDirectory = outputDirectory;
        }

        public async Task Generate(Type type)
        {
            var name = type.Name.Pluralize();

            var (document, root) = await _editor.CreateDocument(name);
            var generator = document.Generator;

            var ns = generator.NamespaceDeclaration(_rootNamespace);

            var clazz = generator.ClassDeclaration(name,
                accessibility: Accessibility.Public);


            var usings = new HashSet<string>();
            var properties = new List<SyntaxNode>();
            // var propertyOrdering = new Dictionary<SyntaxNode, int>();

            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Static);
            foreach (var method in methods)
            {
                // var declaration = generator.MethodDeclaration(method.Name,
                //     accessibility: Accessibility.Public,
                //     returnType: SyntaxFactory.ParseTypeName(method.ReturnType.Name));
                var parameters = method.GetParameters().OrderBy(p => p.Position).ToList();
                var declared = parameters.Select(info => $"{TypeString(info.ParameterType)} {info.Name}");
                var passed = parameters.Select(info => info.Name);

                foreach (var t in parameters.Select(p => p.ParameterType).Append(method.ReturnType))
                {
                    usings.Add(t.Namespace);
                    if (t.IsGenericType)
                    {
                        foreach (var typeArgument in t.GenericTypeArguments)
                        {
                            usings.Add(typeArgument.Namespace);
                        }
                    }
                }

                var declaration = SyntaxFactory.ParseMemberDeclaration($@"
        public virtual {TypeString(method.ReturnType)} {method.Name}({string.Join(", ", declared)}) 
            => {type.Name}.{method.Name}({string.Join(", ", passed)});
");
                properties.Add(declaration);
            }

            // properties = properties.OrderBy(p => propertyOrdering[p]).ToList();
            clazz = generator.AddMembers(clazz, properties);
            ns = generator.AddMembers(ns, clazz);
            var usingNodes = usings.Select(u => generator.NamespaceImportDeclaration(u));
            root = generator.AddNamespaceImports(root, usingNodes);
            root = generator.AddMembers(root, ns);

            document.ReplaceNode(document.OriginalRoot, root);
            var text = await _editor.GetTextAsync(document);
            await File.WriteAllTextAsync("Files.cs", text);
        }

        private string TypeString(Type type)
        {
            var b = new StringBuilder();
            if (type.IsArray && type.HasElementType)
            {
                b.Append(TypeString(type.GetElementType()));
                b.Append("[]");
            }
            else if (TypeAliases.TryGetValue(type.FullName!, out var alias))
                b.Append(alias);
            else if (type.IsGenericType)
            {
                b.Append(type.Name.Split("`")[0]);
                b.Append("<");
                foreach (var argument in type.GenericTypeArguments)
                {
                    b.Append(TypeString(argument));
                }

                b.Append(">");
            }
            else
                b.Append(type.Name);

            return b.ToString();
        }
    }

    public class Editor
    {
        private readonly AdhocWorkspace _workspace;
        private readonly Project _newProject;

        public Editor()
        {
            _workspace = new AdhocWorkspace();
            var projectId = ProjectId.CreateNewId();
            var versionStamp = VersionStamp.Create();
            var projectInfo =
                ProjectInfo.Create(projectId, versionStamp, "NewProject", "projName", LanguageNames.CSharp);
            _newProject = _workspace.AddProject(projectInfo);
        }

        public async Task<(SyntaxNode root, DocumentEditor editor)> LoadDocument(string path)
        {
            var sourceText = SourceText.From(await File.ReadAllTextAsync(path));
            var document = _workspace.AddDocument(_newProject.Id, "NewFile.cs", sourceText);
            var root = await document.GetSyntaxRootAsync()!;
            var editor = await DocumentEditor.CreateAsync(document);
            return (root!, editor);
        }

        public async Task<(DocumentEditor editor, SyntaxNode?)> CreateDocument(string name)
        {
            var document = _workspace.AddDocument(_newProject.Id, name, SourceText.From(""));
            var editor = await DocumentEditor.CreateAsync(document);
            return (editor, await document.GetSyntaxRootAsync());
        }

        public async Task<string> GetTextAsync(DocumentEditor editor)
        {
            var document = await Formatter.FormatAsync(editor.GetChangedDocument());
            var text = await document.GetTextAsync();
            return text.ToString();
        }
    }
}