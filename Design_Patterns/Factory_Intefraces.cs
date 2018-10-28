using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Patterns
{
  
    //The Type of Bullet could be requested
    public enum BulletType
    {
        Cannon,
        Gatling
    }

    //Interface from which every bullet inherit (supply the Shoot function)
    public interface IBullet
    {
        bool Active { get; set; }
        float RateOfFire { get;}

        //void Shoot();
    }

    //Factory Pattern: 
    // • creational pattern, it works by producing instances without expose the creational logic to the client
    // • we delegate the instantiation to a static Factory class
    public static class BulletFactory
    {
        static BulletFactory()
        {
            //Register here all IBullet Pools
            //PoolManager.RegisterPool(typeof(BulletCannon), () => new BulletCannon());
        }

        //We receive an IBullet by passing a BulletType enum value (We can't see the specific types of IBullet, cause they're nested and private!)
        public static IBullet Get(BulletType type)
        {
            IBullet toReturn = null;
            switch (type)
            {
                case BulletType.Cannon:
                    toReturn = new BulletCannon(10f);
                    break;
                case BulletType.Gatling:
                    toReturn = new BulletGatling(1000f);
                    break;
                default:
                    break;
            }
            return toReturn;
        }

        //From here we could create various configurations of IBullet
        public class BulletCannon : IBullet
        {
            public bool Active { get; set; }

            float fireRate;
            float IBullet.RateOfFire { get { return fireRate; } }

            public BulletCannon(float Ratio)
            {
                fireRate = Ratio;
            }
        }
        public class BulletGatling : IBullet
        {
            public bool Active { get; set; }

            float fireRate;
            float IBullet.RateOfFire { get { return fireRate; } }

            public BulletGatling(float Ratio)
            {
                fireRate = Ratio;
            }
        }
    }
}
