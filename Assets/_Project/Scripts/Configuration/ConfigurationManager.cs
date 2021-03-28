using UnityEngine;

public class ConfigurationManager
{

    private const string PREFS_SENSIBILITY = "SENSIBILITY";

    private static ConfigurationManager s_Instance;
    public static ConfigurationManager Instance
    {
        get
        {
            if(s_Instance == null)
            {
                s_Instance = new ConfigurationManager();
            }

            return s_Instance;
        }
    }

    private float _sensibility;
    public float Sensibility
    {
        get
        {
            return _sensibility;
        }
        set
        {
            _sensibility = value;
            PlayerPrefs.SetFloat(PREFS_SENSIBILITY, _sensibility);
        }
    }

    private ConfigurationManager()
    {
        _sensibility = PlayerPrefs.GetFloat(PREFS_SENSIBILITY, 1f);
    }

}