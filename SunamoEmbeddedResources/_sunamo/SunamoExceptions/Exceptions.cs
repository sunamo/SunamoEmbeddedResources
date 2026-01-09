// variables names: ok
namespace SunamoEmbeddedResources._sunamo.SunamoExceptions;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
// © www.sunamo.cz. All Rights Reserved.

/// <summary>
/// Exception handling helper class.
/// EN: Provides shared string builders for accumulating exception information.
/// CZ: Poskytuje sdílené string buildery pro akumulaci informací o výjimkách.
/// </summary>
internal sealed partial class Exceptions
{
    #region Other

#region IsNullOrWhitespace
    /// <summary>
    /// String builder for accumulating inner additional information.
    /// EN: Used to build detailed inner exception information messages.
    /// CZ: Používá se pro sestavení detailních zpráv o vnitřních výjimkách.
    /// </summary>
    internal readonly static StringBuilder AdditionalInfoInnerStringBuilder = new();

    /// <summary>
    /// String builder for accumulating additional information.
    /// EN: Used to build additional exception information messages.
    /// CZ: Používá se pro sestavení dodatečných informací o výjimkách.
    /// </summary>
    internal readonly static StringBuilder AdditionalInfoStringBuilder = new();
    #endregion
#endregion
}