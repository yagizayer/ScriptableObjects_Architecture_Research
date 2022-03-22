using UnityEngine;
using static UnityEngine.Debug;

namespace Helpers.Components.Debug
{
    public class OutputTest : MonoBehaviour
    {
        public void Test() => Log("Test");

        public void Test(string message) => Log(message);

        public void Test(int message) => Log(message);

        public void Test(float message) => Log(message);

        public void Test(bool message) => Log(message);

        public void Test(Vector3 message) => Log(message);

        public void Test(Vector2 message) => Log(message);

        public void Test(Quaternion message) => Log(message);

        public void Test(Color message) => Log(message);

        public void Test(object message) => Log(message);
    }
}