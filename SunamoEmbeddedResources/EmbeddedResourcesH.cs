// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoEmbeddedResources;

/// <summary>
///     Require assembly and default namespace.
///     Content is referred like with ResourcesH - with fs path
/// </summary>
public class EmbeddedResourcesH //: IResourceHelper
{
    /*usage:
uri = new Uri("Wpf.Tests.Resources.EmbeddedResource.txt", UriKind.Relative);
GetString(uri.ToString()) - the same string as passed in ctor Uri
     */

    /// <summary>
    ///     For entry assembly
    /// </summary>
    public static EmbeddedResourcesH? Instance = null;

    /// <summary>
    ///     Default namespace for embedded resources
    /// </summary>
    protected string DefaultNamespace { get; set; } = string.Empty;

    /// <summary>
    ///     Entry assembly containing embedded resources
    /// </summary>
    protected Assembly? EntryAssembly { get; set; }

    /// <summary>
    ///     Protected constructor for derived classes
    /// </summary>
    protected EmbeddedResourcesH()
    {
    }

    /// <summary>
    ///     public to use in assembly like SunamoNTextCat
    ///     A2 is name of project, therefore don't insert typeResourcesSunamo.Namespace
    /// </summary>
    /// <param name="entryAssembly">The assembly containing embedded resources</param>
    /// <param name="defaultNamespace">Default namespace for embedded resources</param>
    public EmbeddedResourcesH(Assembly entryAssembly, string defaultNamespace)
    {
        this.EntryAssembly = entryAssembly;
        DefaultNamespace = defaultNamespace;
    }

    /// <summary>
    ///     Gets the current entry assembly, initializing it if necessary
    /// </summary>
    protected Assembly CurrentEntryAssembly
    {
        get
        {
            if (EntryAssembly == null) EntryAssembly = Assembly.GetEntryAssembly()!;
            return EntryAssembly;
        }
    }

    /// <summary>
    ///     Converts a file path to a resource name by combining with default namespace
    /// </summary>
    /// <param name="name">The resource path</param>
    /// <returns>The full resource name</returns>
    public string GetResourceName(string name)
    {
        string resourceName = string.Join(".", DefaultNamespace,
            name.TrimStart('/').Replace("/", "."));
        return resourceName;
    }

    /// <summary>
    ///     If it's file, return its content
    ///     Its for getting string from file, never from resx or another in code variable
    /// </summary>
    /// <param name="name"></param>
    public string GetString(string name)
    {
        var stream = GetStream(name);

        return Encoding.UTF8.GetString(FS.StreamToArrayBytes(stream));
    }

    /// <summary>
    ///     Resources/tidy_config.txt (no assembly)
    /// </summary>
    /// <param name="name">The resource path</param>
    /// <returns>The manifest resource stream</returns>
    public Stream GetStream(string name)
    {
        var resourceName = GetResourceName(name);
        var stream = CurrentEntryAssembly.GetManifestResourceStream(resourceName);
        return stream!;
    }
}