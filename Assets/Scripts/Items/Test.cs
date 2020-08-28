using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Items
{
    public class SomeController : MonoBehaviour
    {
        [SerializeField] private GameObject _template;
        //They are in the scene
        [SerializeField] private GameObject _g1;

        [SerializeField] private Dependency1 _c1;
        [SerializeField] private Dependency2 _c2;



        public void Create()
        {
            // Assuming that the components are attached to the _template object
            var obj = Instantiate(_template);
            obj.GetComponent<Component3>().Initialize(_c1);
            obj.GetComponent<Component4>().Initialize(_c2);
        }
    }

    class Component3 : IInitializeableComponent<Dependency1>
    {
        public void Initialize(Dependency1 data)
        {
            // Do initialization for Component 3 here
            throw new NotImplementedException();
        }
    }

    class Component4 : IInitializeableComponent<Dependency2>
    {
        public void Initialize(Dependency2 data)
        {
            // Do initialization for Component 4 here
            throw new NotImplementedException();
        }
    }

    interface IInitializeableComponent<T>
    {
        // This interface is technically not necessary
        // But tby creating this contract for how a component like this should work
        // Will help us in the future if we want to change things.
        // The code will tell us "you are breaking a contract" and you will have to
        // make a concious decision about changing what ComponentX should actually depend on
        void Initialize(T data);
    }

    class Dependency1
    {

    }

    class Dependency2
    {

    }

    
}
