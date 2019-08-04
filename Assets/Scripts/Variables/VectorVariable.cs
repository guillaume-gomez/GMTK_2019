using UnityEngine;
using UnityEditor;

namespace RoboRyanTron.Unite2017.Variables
{
    [CreateAssetMenu]
    public class VectorVariable : ScriptableObject
    {
        [SerializeField] private Vector3 value = Vector3.zero;

        public Vector3 Value
        {
            get { return value; }
            set { this.value = value; }
        }
    }
}