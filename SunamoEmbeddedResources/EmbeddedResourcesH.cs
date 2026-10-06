namespace SunamoEmbeddedResources;

/// <summary>
///     Require assembly and default namespace.
///     Content is referred like with ResourcesH - with fs path
/// </summary>
/*usage:
uri = new Uri("Wpf.Tests.Resources.EmbeddedResource.txt", UriKind.Relative);
GetString(uri.ToString()) - the same string as passed in ctor Uri
    /// <summary>
    ///     For entry assembly
    /// </summary>
 */
public class EmbeddedResourcesH //: IResourceHelper
{
    public static EmbeddedResourcesH? Instance = null;

    // Default namespace for embedded resources
    protected string DefaultNamespace { get; set; } = string.Empty;

    // Entry assembly containing embedded resources
    protected Assembly? EntryAssembly { get; set; }

    protected EmbeddedResourcesH()
    {
    }

    // public to use in assembly like SunamoNTextCat
    // A2 is name of project, therefore don't insert typeResourcesSunamo.Namespace
    public EmbeddedResourcesH(Assembly entryAssembly, string defaultNamespace)
    {
        this.EntryAssembly = entryAssembly;
        DefaultNamespace = defaultNamespace;
    }

    protected Assembly CurrentEntryAssembly
    {
        get
        {
            if (EntryAssembly == null) EntryAssembly = Assembly.GetEntryAssembly()!;
            return EntryAssembly;
        }
    }

    // Converts a file path to a resource name by combining with default namespace
    public string GetResourceName(string path)
    {
        var resourceName = string.Join(".", DefaultNamespace,
            path.TrimStart('/').Replace("/", "."));
        return resourceName;
    }

    // If it's file, return its content
    // Its for getting string from file, never from resx or another in code variable
    public string GetString(string path)
    {
        var stream = GetStream(path);
        return Encoding.UTF8.GetString(FS.StreamToArrayBytes(stream));
    }

    // Resources/tidy_config.txt (no assembly)
    public Stream GetStream(string path)
    {
        var resourceName = GetResourceName(path);
        var stream = CurrentEntryAssembly.GetManifestResourceStream(resourceName);
        return stream!;
    }
}
