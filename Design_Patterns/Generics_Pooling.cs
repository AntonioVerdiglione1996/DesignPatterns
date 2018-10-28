using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Patterns
{
    public class Pool<T> where T : class
    {
        private Queue<T> pool;

        public Pool()
        {
            pool = new Queue<T>();
        }

        public T Get(Action activator = null)
        {
            if (pool.Count > 0)
            {
                T ItemFromQueue = pool.Dequeue();
                if (activator != null)
                {
                    activator.Invoke();
                }
                return ItemFromQueue;
            }

            T newItem = (T)Activator.CreateInstance(typeof(T));
            return newItem;
        }
        public void Recycle(T itemToRecycle, Action resetter = null)
        {
            if (resetter != null)
                resetter.Invoke();
            pool.Enqueue(itemToRecycle);
        }
    }
}
