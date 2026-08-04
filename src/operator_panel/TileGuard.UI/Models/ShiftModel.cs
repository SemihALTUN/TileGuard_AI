using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileGuard.UI.Models
{
    public class ShiftModel
    {
        public int ShiftId { get; set; }
        public string ShiftName { get; set; } = string.Empty;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsActive { get; set; } = true;
        public override string ToString()
        {
            return $"{ShiftName} ({StartTime:hh\\:mm} - {EndTime:hh\\:mm})";
        }
    }
}
