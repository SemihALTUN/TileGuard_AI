using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileGuard.UI.Models
{
    public class ShiftStatsModel
    {
        public int TotalInspected { get; set; }
        public int OkCount { get; set; }
        public int NokCount { get; set; }

        public double DefectRate => TotalInspected > 0 ? ((double)NokCount / TotalInspected) * 100 : 0;
    }
}
