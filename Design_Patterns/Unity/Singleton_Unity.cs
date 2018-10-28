using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Patterns
{
    //we cannot inherit
    sealed public class GameManager
    {
        //we create a static instance
        public static GameManager Instance;
        //in the awake callBack method of unity initialize the instance field
        void Awake()
        {
            Instance = this;
        }
    }
}
