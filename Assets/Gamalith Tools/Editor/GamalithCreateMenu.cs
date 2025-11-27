using UnityEditor;

public static class GamalithCreateMenu
{
    // Order values (last int) are just for sorting inside "C# Templates"

    [MenuItem("Assets/Create/C# Templates/Interface Script", false, 1)]
    public static void CreateInterface()
    {
        const string templatePath = "Assets/Gamalith Tools/ScriptTemplates/InterfaceTemplate.cs.txt";
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, "NewInterface.cs");
    }

    [MenuItem("Assets/Create/C# Templates/Struct Script", false, 2)]
    public static void CreateStruct()
    {
        const string templatePath = "Assets/Gamalith Tools/ScriptTemplates/StructTemplate.cs.txt";
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, "NewStruct.cs");
    }

    [MenuItem("Assets/Create/C# Templates/Plain Class", false, 3)]
    public static void CreatePlainClass()
    {
        const string templatePath = "Assets/Gamalith Tools/ScriptTemplates/PlainClassTemplate.cs.txt";
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, "NewClass.cs");
    }

    [MenuItem("Assets/Create/C# Templates/MonoBehaviour", false, 4)]
    public static void CreateMonoBehaviour()
    {
        const string templatePath = "Assets/Gamalith Tools/ScriptTemplates/MonoBehaviourTemplate.cs.txt";
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, "NewMonoBehaviour.cs");
    }

    [MenuItem("Assets/Create/C# Templates/MonoBehaviour (Regions)", false, 5)]
    public static void CreateMonoBehaviourRegions()
    {
        const string templatePath = "Assets/Gamalith Tools/ScriptTemplates/MonoBehaviourRegionsTemplate.cs.txt";
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, "NewMonoBehaviourWithRegions.cs");
    }

    [MenuItem("Assets/Create/C# Templates/ScriptableObject", false, 6)]
    public static void CreateScriptableObject()
    {
        const string templatePath = "Assets/Gamalith Tools/ScriptTemplates/ScriptableObjectTemplate.cs.txt";
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, "NewScriptableObject.cs");
    }

    [MenuItem("Assets/Create/C# Templates/Event Channel", false, 7)]
    public static void CreateEventChannel()
    {
        const string templatePath = "Assets/Gamalith Tools/ScriptTemplates/EventChannelTemplate.cs.txt";
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, "NewEventChannel.cs");
    }

    [MenuItem("Assets/Create/C# Templates/Enum", false, 8)]
    public static void CreateEnum()
    {
        const string templatePath = "Assets/Gamalith Tools/ScriptTemplates/EnumTemplate.cs.txt";
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, "NewEnum.cs");
    }

    [MenuItem("Assets/Create/C# Templates/Static Utility Class", false, 9)]
    public static void CreateStaticUtility()
    {
        const string templatePath = "Assets/Gamalith Tools/ScriptTemplates/StaticUtilityTemplate.cs.txt";
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, "NewUtility.cs");
    }

    [MenuItem("Assets/Create/C# Templates/Mono Singleton", false, 10)]
    public static void CreateMonoSingleton()
    {
        const string templatePath = "Assets/Gamalith Tools/ScriptTemplates/MonoSingletonTemplate.cs.txt";
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, "NewSingleton.cs");
    }
}
