using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Text.RegularExpressions;

public sealed class ScriptAssetKeywordsReplacer : UnityEditor.AssetModificationProcessor
{
    public static readonly string[] applyFolder = {
        "Exercise_UI"
        };
    /// <summary>
    /// Build project path nodes tree for ignoring path in namespace.
    /// Leave 'Children' unset (or empty) to indicate end (leaf) node.
    /// </summary>
    private static readonly PathNode IgnoredPathTree = new PathNode("Assets")
    {
        Children = new PathNode[]
        {
                new PathNode("Scripts")
                {
                    Children = new PathNode[]
                    {
                        new PathNode("Domain"),
                        new PathNode("Application"),
                        new PathNode("Infrastructure")
                    }
                }
        }
    };


    /// <summary>
    ///  This gets called for every .meta file created by the Editor.
    /// </summary>
    public static void OnWillCreateAsset(string path)
    {
        path = path.Replace(".meta", string.Empty);

        if (!path.EndsWith(".cs") ||
        applyFolder.FirstOrDefault(x => path.Contains(x)) == null
        )
        {
            return;
        }

        var systemPath = path.Insert(0, Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("Assets")));
        // if (applyFolder.FirstOrDefault(x => path.Contains(x)) == null)
        // {
        //     CheckRemoveTemplateNameSpace(systemPath, path);
        //     return;
        // }


        ReplaceScriptKeywords(systemPath, path);


        AssetDatabase.Refresh();
    }

    private static void CheckRemoveTemplateNameSpace(string systemPath, string projectPath)
    {

        var fileData = File.ReadAllText(systemPath);
        if (fileData.IndexOf("#NAMESPACE#") <= -1)
        {
            return;
        }
        // fileData = fileData.Replace("#NAMESPACE#", fullNamespace);
        File.WriteAllText(systemPath, fileData);
    }

    private static string AddPathSign(string pathString)
    {
        return pathString.EndsWith("/") ? pathString : pathString + "/";
    }

    private static string BuildNamespace(string systemPath)
    {
        var parts = new List<string>();

        if (!string.IsNullOrWhiteSpace(EditorSettings.projectGenerationRootNamespace))
        {
            parts.Add(EditorSettings.projectGenerationRootNamespace);
        }

        if (!string.IsNullOrWhiteSpace(systemPath))
        {
            parts.Add(systemPath);
        }

        var fullNamespace = string.Join(".", parts);

        return string.IsNullOrWhiteSpace(fullNamespace)
            ? "RootNamespaceNotSet"
            : fullNamespace;
    }

    private static void ReplaceScriptKeywords(string systemPath, string projectPath)
    {
        projectPath = projectPath.Substring(0, projectPath.LastIndexOf('/'));
        projectPath = TrimPathMembers(projectPath, IgnoredPathTree).Replace('/', '.').Trim('.');
        // projectPath = projectPath.Replace(".Scripts.", ".").Replace(".Script.", ".");
        var subPath = projectPath.Split('.').ToList();
        var fullNamespace = BuildNamespace(projectPath);
        fullNamespace = string.Join(".", projectPath.Split('.').Where(x => x.ToLower() != "script"
        && x.ToLower() != "scripts"
        ));

        // if (projectPath.IndexOf(".Script") > 0)
        // {
        //     projectPath = projectPath.Substring(0, projectPath.IndexOf(".Script"));
        // }


        var fileData = File.ReadAllText(systemPath);
        if (fileData.IndexOf("#NAMESPACE#") > -1)
        {
            fileData = fileData.Replace("#NAMESPACE#", fullNamespace);
        }
        else if (fileData.IndexOf("namespace") > -1)
        {
            return;
        }
        else
        {
            var m = Regex.Match(fileData, @"using[^;]*;", RegexOptions.RightToLeft);
            fileData = fileData.Substring(0, m.Index + m.Length) + Regex.Replace(fileData.Substring(m.Index + m.Length), "(\r\n|\r|\n)", "\r\n\t");
            fileData = fileData.Insert(m.Index + m.Length, $"\r\n\r\nnamespace {fullNamespace}\r\n{{");
            fileData = fileData + "\r\n}";
        }

        File.WriteAllText(systemPath, fileData);
    }

    private static string TrimPathMembers(string projectPath, PathNode pathNode)
    {
        if (pathNode == null || !pathNode.Matches(projectPath))
        {
            return projectPath;
        }

        var trimString = projectPath.Length > pathNode.Name.Length
            ? AddPathSign(pathNode.Name)
            : pathNode.Name;

        projectPath = projectPath.Remove(0, trimString.Length);

        if (!pathNode.IsLeaf)
        {
            foreach (var childNode in pathNode.Children)
            {
                projectPath = TrimPathMembers(projectPath, childNode);
            }
        }

        return projectPath;
    }


    private sealed class PathNode
    {
        public string Name { get; } = string.Empty;
        public PathNode[] Children { get; set; } = new PathNode[0];

        public bool IsLeaf => Children == null || Children.Length == 0;


        public PathNode(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            // Forcing name to NOT end with '/'
            this.Name = name.EndsWith("/")
                ? name.Substring(name.Length - 2, 1)
                : name;
        }


        public bool Matches(string path)
        {
            return !string.IsNullOrWhiteSpace(Name) &&
                path.StartsWith(Name, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
