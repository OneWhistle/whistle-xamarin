using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whistle.Core.Modal
{
    public static class RideTypeConstants
    {
        public const string BIKE = "BIKE";
        public const string AUTO = "AUTO";
        public const string SMALL_CAR = "SMALL_CAR";
        public const string LARGE_CAR = "LARGE_CAR";
        public const string MINI_BUS = "MINI_BUS";
        public const string BUS = "BUS";
        public const string TRUCK = "TRUCK";
        public const string TRAIN = "TRAIN";
        public const string FLIGHT = "FLIGHT";

        public static string[] All = new[]
        {
BIKE      ,
AUTO      ,
SMALL_CAR ,
LARGE_CAR ,
MINI_BUS  ,
BUS       ,
TRUCK     ,
TRAIN     ,
FLIGHT    ,
        };

    }

}
