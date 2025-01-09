using UnityEngine;
namespace DefaultNamespace
{
    public class Mivent
    {
        public GameObject eventObject;
        virtual public void SetObject(GameObject obj)
        {
            eventObject = obj;
        }
    }

    public class Texvent : Mivent
    {
        public override void SetObject(GameObject obj)
        {
            base.SetObject(obj);
        }
    }
}