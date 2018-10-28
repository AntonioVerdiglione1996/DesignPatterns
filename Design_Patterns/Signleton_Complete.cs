using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Patterns
{
    
    //public and sealed class.
        //public- we need to access this class in every point of the cose
        //sealed- this class cannot be inherited
    sealed public class RandomManager
    {
        private static RandomManager instance;
        public static RandomManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new RandomManager();
                return instance;
            }
        }
        private RandomManager()
        {
            Random = new Random();
        }

        public Random Random;
    }
    // in the code we call: RandomManager.Instance.Random.Next(a,b);
}
