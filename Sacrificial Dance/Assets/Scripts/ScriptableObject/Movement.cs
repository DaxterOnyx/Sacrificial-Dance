using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Movement", order = 1)]
public class Movement : ScriptableObject
{
   public KeyCode key = KeyCode.A;
   public Sprite sprite;
   public Sprite callSprite;
}
