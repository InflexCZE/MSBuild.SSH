using System;
using System.Collections.Generic;
using System.IO;

namespace MSBuild.SSH.Utils;

public static class PathUtils
{
    public static char[] DirectorySeparators = new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar };

    public static bool IsRootedPath(string? path)
    {
        return path?.StartsWith("/") == true;
    }

    public static string CanonizePath(string? path, string homePath)
    {
        if (IsRootedPath(path) == false)
        {
            if (path?.StartsWith("./") == true || path?.StartsWith(".\\") == true)
            {
                path = path.Substring(2, path.Length - 2);
            }

            if (string.IsNullOrEmpty(path))
            {
                path = homePath;
            }
            else
            {
                path = Path.Combine(homePath, path);
            }
        }

        path = path!.Replace('\\', '/');

        if (path.EndsWith("/."))
        {
            path = path.Substring(0, path.Length - 2);
        }

        return path;
    }

    public static IEnumerable<string> IncrementalPathSegments(string fullPath)
    {
        var segments = fullPath.Split(DirectorySeparators, StringSplitOptions.RemoveEmptyEntries);

        var path = IsRootedPath(fullPath) ? "/" : string.Empty;
        foreach (var segment in segments)
        {
            if (segment == ".")
                continue;

            path += segment + "/";
            yield return path;
        }
    }
}