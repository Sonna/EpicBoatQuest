using UnityEngine;
using Leap;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    private Controller m_leapController;

    public GUISkin MainMenuSkin;
    public float Width;
    public float Height;
    public string gameName = "A-Boat Life";
    public string[] gameInstructions;

    public Texture2D background;

    private int toolbarInt = 0;
    private string[]  toolbarstrings =  {"Audio","Graphics","System"};
    private Menu CurrentMenu;
    private enum Menu
    {
        Main,
        Controls,
        Options
    }

    // Use this for initialization
    void Start ()
    {
        m_leapController = new Controller();

        CurrentMenu = Menu.Main;
        if (Width == 0 ) { Width = (UnityEngine.Screen.width * 0.5f); }
        if (Height == 0 ) { Height = (UnityEngine.Screen.height * 0.5f); }
    }

    // Update is called once per frame
    void OnGUI ()
    {
        Frame frame = m_leapController.Frame();

        if (frame.Hands.Count >= 1)
        {
            if(frame.Hands[0].PalmVelocity.Magnitude >= 1000.0f)
            {
                Application.LoadLevel("Level01");
            }
        }

        GUI.skin = MainMenuSkin;

        // Get half the screen and desired GUI item width
        float ScreenX = (float)((UnityEngine.Screen.width * 0.5) - (Width * 0.5));
        float ScreenY = (float)((UnityEngine.Screen.height) - (Height * 0.5));

        if (CurrentMenu == Menu.Main)
        {
            GUI.Box(new Rect (ScreenX, ScreenY, Width, Height), background);
            GUILayout.BeginArea(new Rect (ScreenX, ScreenY, Width, Height));
            GUILayout.Label(gameName);

                //Menu buttons
                if (GUILayout.Button("Wave to begin or click Start"))
                {
                    Application.LoadLevel("Level01"); //Change this to the level that is required!
                }
                if (GUILayout.Button("Controls"))
                {
                    CurrentMenu = Menu.Controls;
                }
                if (GUILayout.Button("Options"))
                {
                    CurrentMenu = Menu.Options;
                }
                if (GUILayout.Button("Quit"))
                {
                    Application.Quit();
                }

            GUILayout.EndArea();
        }

        if (CurrentMenu == Menu.Controls)
        {
            GUILayout.BeginArea (new Rect (ScreenX, ScreenY, Width, Height));
            GUILayout.Label(gameName);

            foreach(string label in gameInstructions)
            {
                GUILayout.Box(label);
            }

            if (GUILayout.Button ("Return"))
            {
                CurrentMenu = Menu.Main;
            }

            GUILayout.EndArea ();
        }

        /* CODECLEANUP
        * No Reference Link
        */

        //COPIED FROM INTERNET SCRIPT IN PauseMenu.cs

        if (CurrentMenu == Menu.Options)
        {
            GUILayout.BeginArea (new Rect (ScreenX, ScreenY, Width, Height));
            toolbarInt = GUILayout.Toolbar (toolbarInt, toolbarstrings);

            switch (toolbarInt)
            {
            case 0:
                VolumeControl();
                break;
            case 2:
                ShowDevice();
                break;
            case 1:
                Qualities();
                QualityControl();
                break;
            default:
                break;
            }

            if (GUILayout.Button ("Return"))
            {
                CurrentMenu = Menu.Main;
            }

            GUILayout.EndArea ();
        }
    }

    private void ShowDevice()
    {
        GUILayout.Box("Unity player version "+Application.unityVersion);
        GUILayout.Box("Graphics: "+SystemInfo.graphicsDeviceName+" "+
                            SystemInfo.graphicsMemorySize+"MB\n"+
                            SystemInfo.graphicsDeviceVersion+"\n"+
                            SystemInfo.graphicsDeviceVendor);
        GUILayout.Box("Shadows: "+SystemInfo.supportsShadows);
        GUILayout.Box("Image Effects: "+SystemInfo.supportsImageEffects);
        GUILayout.Box("Render Textures: "+SystemInfo.supportsRenderTextures);
    }

    private void Qualities()
    {
        int currentLevel = QualitySettings.GetQualityLevel();

        GUILayout.Label(QualitySettings.names[currentLevel]);
    }

    private void QualityControl()
    {
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Decrease"))
        {
            QualitySettings.DecreaseLevel();
        }
        if (GUILayout.Button("Increase"))
        {
            QualitySettings.IncreaseLevel();
        }
        GUILayout.EndHorizontal();
    }

    private void VolumeControl()
    {
        GUILayout.Label("Volume");
        // Creates a slider between zero (no sound) and one (max volume)
        AudioListener.volume = GUILayout.HorizontalSlider(AudioListener.volume, 0, 1);
    }
}
