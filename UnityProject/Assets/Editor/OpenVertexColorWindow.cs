using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using ProBuilder2.EditorCommon;

public class OpenVertexColorWindow : Editor
{
    /**
	 *	Define the shortcut with whatever modifier keys you'd like.
	 * 	https://docs.unity3d.com/ScriptReference/MenuItem.html
	 */
    [MenuItem("Tools/ProBuilder/Editors/Open Vertex Painter %&v")]
    static void MenuOpenVertexColorWindow()
    {
        // Open the preferred vertex color editor.
        pb_Menu_Commands.MenuOpenVertexColorsEditor();

        // Or if you want to be specific about which to open:

        // This opens the toolbar
        // pb_Vertex_Color_Toolbar.MenuOpenWindow();
        // This opens the painter
        // pb_VertexColor_Editor.MenuOpenWindow();
    }
}
