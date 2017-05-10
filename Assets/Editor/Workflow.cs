using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Workflow : Editor {

    [MenuItem("GameObject/Group %g")]
    static void GroupObjects ()
    {
        Transform[] transforms = Selection.GetTransforms(SelectionMode.TopLevel);

        GameObject newParent = new GameObject("Group");
        Undo.RegisterCreatedObjectUndo(newParent, "create group parent");

        for (int i = 0; i < transforms.Length; i++)
        {
            Undo.SetTransformParent(transforms[i], newParent.transform, "set's parent to group");
        }

    }



}
