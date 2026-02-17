using UnityEngine;

public static class ExtensionGameObject
{
    public static bool SafeIsNull(this Object @object)
    {
        if (@object == null || @object.Equals(null))
            return true;

        return false;
    }

    public static bool SafeIsNull(this Object[] @objects)
    {
        if (@objects == null || @objects.Equals(null))
            return true;

        return false;
    }

    public static void SafeSetActive(this Component component, bool active)
    {
        if (component.SafeIsNull())
            return;

        if (component.gameObject.SafeIsNull())
            return;

        component.gameObject.SetActive(active);
    }

    public static void SafeSetActive(this Component[] components, bool active)
    {
        if (components.SafeIsNull())
            return;

        for (int i = 0; i < components.Length; ++i)
            components[i].SafeSetActive(active);
    }

    public static void ChangeLayer(this MonoBehaviour mono, string name, bool isChild)
    {
        if (mono == null)
            return;

        mono.gameObject.layer = LayerMask.NameToLayer(name);

        Transform[] childs = mono.GetComponentsInChildren<Transform>(true);
        for (int i = 0; i < childs.Length; ++i)
        {
            if (childs[i] == null)
                continue;

            childs[i].gameObject.layer = LayerMask.NameToLayer(name);
        }
    }
}
