using RT.Util.Controls;

namespace ExpertSokoban
{
    /// <summary>
    /// Encapsulates a menu item group that selects a value from the PathDrawMode enum.
    /// </summary>
    sealed class MenuRadioGroupPathDrawMode : MenuRadioGroup<PathDrawMode> { }

    /// <summary>
    /// Encapsulates a menu item group that selects a value from the MainAreaTool enum.
    /// </summary>
    sealed class MenuRadioGroupMainAreaTool : MenuRadioGroup<MainAreaTool> { }

    /// <summary>
    /// Encapsulates a menu item that selects a value from the PathDrawMode enum.
    /// </summary>
    sealed class MenuRadioItemPathDrawMode : MenuRadioItem<PathDrawMode> { }

    /// <summary>
    /// Encapsulates a menu item that selects a value from the MainAreaTool enum.
    /// </summary>
    sealed class MenuRadioItemMainAreaTool : MenuRadioItem<MainAreaTool> { }
}
