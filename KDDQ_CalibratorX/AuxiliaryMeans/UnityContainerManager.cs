using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using KDDQ_CalibratorX.MetersStandards;
using System.ComponentModel;
using CalibrateFlow;
using Unity.Injection;

namespace KDDQ_CalibratorX.AuxiliaryMeans
{    

    public class UnityContainerManager
    {
        private static IUnityContainer _container;

        static UnityContainerManager()
        {
            _container = new UnityContainer();
            // 在这里注册你的设备类
            _container.RegisterType<IDevice, M143M>("M143M", new InjectionConstructor(typeof(string)));
            _container.RegisterType<IDevice, KDDQ_Y>("KDDQ-Y", new InjectionConstructor(typeof(string)));


            _container.RegisterSingleton<CalibrateFlowOperation>();
        }

        public static IUnityContainer GetContainer()
        {
            return _container;
        }
    }

}
