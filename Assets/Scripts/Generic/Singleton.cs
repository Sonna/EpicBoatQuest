using UnityEngine;
using System.Collections;

// > Use singletons for convenience. The following class will make any class
// > that inherits from it a singleton automatically:
// >
// > Singletons are useful for managers, such as `ParticleManager` or
// > `AudioManager` or `GUIManager`.
// >
// > - Avoid using singletons for unique instances of prefabs that are not
// >   managers (such as the Player). Not adhering to this principle complicates
// >   inheritance hierarchies, and makes certain types of changes harder.
// >   Rather keep references to these in your GameManager (or other suitable
//>    God class ;-) )
// >
// > - Define static properties and methods for public variables and methods
// >   that are used often from outside the class. This allows you to write
// >   `GameManager.Player` instead of `GameManager.Instance.player`.
//
// Example of use:
//     public sealed class MyClass : Singleton<MyClass>
//     { ... }
//
// References:
// - http://devmag.org.za/2012/07/12/50-tips-for-working-with-unity-best-practices/

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance;


    // Returns the instance of this singleton.
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = (T) FindObjectOfType(typeof(T));

                if (instance == null)
                {
                    Debug.LogError("An instance of " + typeof(T) + " is needed in the scene, but there is none.");
                }
            }

            return instance;
        }
    }
}
