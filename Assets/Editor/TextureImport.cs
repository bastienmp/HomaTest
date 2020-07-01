using UnityEditor;

public class TextureImport : AssetPostprocessor
{
    void OnPreprocessTexture()
    {
        TextureImporter textureImporter = (TextureImporter)assetImporter;
        textureImporter.maxTextureSize = 512;
        textureImporter.textureCompression = TextureImporterCompression.CompressedHQ;

        int startIndex = assetPath.LastIndexOf("/") + 1;
        string assetName = assetPath.Substring(startIndex, assetPath.Length - startIndex);

        if (assetName.StartsWith("s_"))
        {
            textureImporter.textureType = TextureImporterType.Sprite;
        }
        else
        {
            textureImporter.textureType = TextureImporterType.Default;
        }
    }
}