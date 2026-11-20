namespace SunamoEmbeddedResources;

/// <summary>
///     Require assembly and default namespace.
///     Content is referred like with ResourcesH - with fs path
/// </summary>
/*usage:
uri = new Uri("Wpf.Tests.Resources.EmbeddedResource.txt", UriKind.Relative);
GetString(uri.ToString()) - the same string as passed in ctor Uri
 */
public class EmbeddedResourcesH //: IResourceHelper
{
    /// <summary>
    /// The instance.
    /// </summary>
    public static EmbeddedResourcesH? Instance = null;

    // Default namespace for embedded resources
    /// <summary>
    /// Gets or sets the default namespace.
    /// </summary>
    protected string DefaultNamespace { get; set; } = string.Empty;

    // Entry assembly containing embedded resources
    /// <summary>
    /// Gets or sets the entry assembly.
    /// </summary>
    protected Assembly? EntryAssembly { get; set; }

    /// <summary>
    /// Embedded resources h.
    /// </summary>
    /// <returns>The embedded resources h result.</returns>
    protected EmbeddedResourcesH()
    {
    }

    // public to use in assembly like SunamoNTextCat
    // A2 is name of project, therefore don't insert typeResourcesSunamo.Namespace
    /// <summary>
    /// Embedded resources h.
    /// </summary>
    /// <returns>The embedded resources h result.</returns>
    public EmbeddedResourcesH(Assembly entryAssembly, string defaultNamespace)
    {
        this.EntryAssembly = entryAssembly;
        DefaultNamespace = defaultNamespace;
    }

    /// <summary>
    /// The current entry assembly.
    /// </summary>
    protected Assembly CurrentEntryAssembly
    {
        get
        {
            if (EntryAssembly == null) EntryAssembly = Assembly.GetEntryAssembly()!;
            return EntryAssembly;
        }
    }

    // Converts a file path to a resource name by combining with default namespace
    /// <summary>
    /// Get resource name.
    /// </summary>
    /// <param name="EntryAssembly">The entry assembly.</param>
    /// <returns>The get resource name result.</returns>
    public string GetResourceName(string path)
    {
        var resourceName = string.Join(".", DefaultNamespace,
            path.TrimStart('/').Replace("/", "."));
        return resourceName;
    }

    // If it's file, return its content
    // Its for getting string from file, never from resx or another in code variable
    /// <summary>
    /// Get string.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns>The get string result.</returns>
    public string GetString(string path)
    {
        var stream = GetStream(path);
        return Encoding.UTF8.GetString(FS.StreamToArrayBytes(stream));
    }

    // Resources/tidy_config.txt (no assembly)
    /// <summary>
    /// Get stream.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns>The get stream result.</returns>
    public Stream GetStream(string path)
    {
        var resourceName = GetResourceName(path);
        var stream = CurrentEntryAssembly.GetManifestResourceStream(resourceName);
        return stream!;
    }
}
