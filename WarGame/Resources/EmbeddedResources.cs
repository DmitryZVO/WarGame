﻿using System.Reflection;

namespace WarGame.Resources;

public static class EmbeddedResources
{
    private static readonly Dictionary<Type, Func<Stream, object?>> Types = new()
    {
        { typeof(string), s => { using var sr = new StreamReader(s); return sr.ReadToEnd(); } },
        { typeof(Bitmap), s => new Bitmap(s) },
        { typeof(Icon), s => new Icon(s) }
    };

    public static T? Get<T>(string fileName)
    {
        var fullName = $"{typeof(EmbeddedResources).Namespace}.{fileName}";
        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fullName);
        if (stream == null) return default;
        if (!Types.TryGetValue(typeof(T), out var func)) return default;

        return (T?)func(stream);
    }
}