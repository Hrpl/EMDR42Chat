using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMDR42Chat.Domain.Commons.DTO;

public class ChatDataDTO
{
    public bool IsShowInterface { get; set; }
    public int ActiveTab { get; set; }
    public int SpeedObject { get; set; } = 30;
    public int SecondsTimer { get; set; }
    public int RepeatTimer { get; set; }
    public bool SwitchStartObject { get; set; } = false;
    public int QualObjects { get; set; } = 1;
    public int HeightObject { get; set; } = 40;
    public int SizeObject { get; set; } = 35;
    public int TypeSound { get; set; }
    public int Volume { get; set; }
    public int SoundMode { get; set; }
    public int Effect { get; set; }
    public int Repeat { get; set; }
    public int View { get; set; }
    public int TypeAnimation { get; set; } = 1;
    public int Figure { get; set; } = 1;

    public string Color { get; set; } = "1";
    public string ColorCenter { get; set; } = "1";
    public string Background { get; set; } = "1";
}
