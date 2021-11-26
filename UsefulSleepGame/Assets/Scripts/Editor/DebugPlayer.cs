// using UnityEngine;
// using UnityEditor;
// using UnityEngine.Experimental.Rendering.Universal;

// public class DebugPlayer : EditorWindow
// {
//     private Vector2 scrollPos = Vector2.zero;

//     [MenuItem("Window/DebugPlayer")]
//     public static void ShowWindow()
//     {
//         GetWindow<DebugPlayer>();
//     }

//     private void OnGUI()
//     {
//         {
//             PlayerManager player = PlayerManager.Instance;
//             Light2D globalLight = GameObject.Find("Environment/Lighting/GlobalLight").GetComponent<Light2D>();

//             scrollPos = GUILayout.BeginScrollView(scrollPos, false, true);

//             GUILayout.Label("Resources", EditorStyles.boldLabel);

//             player.Coin = EditorGUILayout.IntField("Coin", player.Coin);
//             player.Wood = EditorGUILayout.IntField("Wood", player.Wood);
//             player.Stone = EditorGUILayout.IntField("Stone", player.Stone);
//             player.Wool = EditorGUILayout.IntField("Wool", player.Wool);

//             DrawUILine();

//             GUILayout.Label("Player State", EditorStyles.boldLabel);

//             player.Temperature = EditorGUILayout.IntField("Temperature", player.Temperature);
//             player.ambientTemperature = EditorGUILayout.IntField("Ambient Temperature", player.ambientTemperature);
//             // GameManager.checkpoint.position = EditorGUILayout.Vector3Field("Checkpoint Loc", GameManager.checkpoint.position);
//             // GameManager.checkpoint.sceneName = EditorGUILayout.TextField("Checkpoint Scene", GameManager.checkpoint.sceneName);


//             GUILayout.Space(10f);

//             player.canMove = EditorGUILayout.Toggle("canMove", player.canMove);
//             player.canJump = EditorGUILayout.Toggle("canJump", player.canJump);
//             player.canSpeak = EditorGUILayout.Toggle("canSpeak", player.canSpeak);
//             player.canTrade = EditorGUILayout.Toggle("canTrade", player.canTrade);
//             player.canAttack = EditorGUILayout.Toggle("canAttack", player.canAttack);
//             player.isOnGround = EditorGUILayout.Toggle("isOnGround", player.isOnGround);
//             player.isInWater = EditorGUILayout.Toggle("isInWater", player.isInWater);
//             player.IsSpeaking = EditorGUILayout.Toggle("IsSpeaking", player.IsSpeaking);
//             player.IsTrading = EditorGUILayout.Toggle("IsTrading", player.IsTrading);

//             DrawUILine();

//             GUILayout.Label("Game State", EditorStyles.boldLabel);

//             GameManager.isPaused = EditorGUILayout.Toggle("isPaused", GameManager.isPaused);

//             DrawUILine();

//             GUILayout.Label("Misc Debug", EditorStyles.boldLabel);

//             globalLight.intensity = EditorGUILayout.FloatField("Intensity", globalLight.intensity);

//             GUILayout.EndScrollView();
//         }
//     }

//     public static void DrawUILine()
//     {
//         EditorGUILayout.Space();
//         var rect = EditorGUILayout.BeginHorizontal();
//         Handles.color = Color.gray;
//         Handles.DrawLine(new Vector2(rect.x - 15, rect.y), new Vector2(rect.width + 15, rect.y));
//         EditorGUILayout.EndHorizontal();
//         EditorGUILayout.Space();
//     }
// }