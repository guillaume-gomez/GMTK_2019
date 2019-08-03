using UnityEngine;

namespace RoboRyanTron.Unite2017.Variables
{
    [CreateAssetMenu]
    public class BoolVariable : ScriptableObject
    {
        [SerializeField] private bool value = true;

        public bool Value
        {
            get { return value; }
            set { this.value = value; }
        }
    }
}