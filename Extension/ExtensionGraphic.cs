using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class ExtensionGraphic
{
    #region Sprite
    public static void SafeSetColor(this Graphic graphic, Color color)
    {
        if (graphic.SafeIsNull())
            return;

        graphic.color = color;
    }

    public static void SafeSetSprite(this Image image, Sprite sprite)
    {
        if (image.SafeIsNull())
            return;

        image.sprite = sprite;
    }

    public static void SafeSetSpriteNativeSize(this Image image, string path)
    {
        if (image.SafeIsNull())
            return;

        image.SetNativeSize();
    }
    #endregion

    #region Text
    public static void SafeSetText(this Text text, string value)
    {
        if (text.SafeIsNull())
            return;

        text.text = value;
    }

    public static void SafeSetText(this TextMeshProUGUI text, string value)
    {
        if (text.SafeIsNull())
            return;

        text.text = value;
    }
    #endregion
}
