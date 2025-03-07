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
    public int SpeedObject { get; set; } 
    public int SecondsTimer { get; set; }
    public int RepeatTimer { get; set; }
    public bool SwitchStartObject { get; set; }
    public int QualObjects { get; set; }
    public int HeightObject { get; set; }
    public int SizeObject { get; set; }
    public int TypeSound { get; set; }
    public int Volume { get; set; }
    public int SoundMode { get; set; }
    public int Efect { get; set; }
    public int Repeat { get; set; }
    public int View { get; set; }
    public int TypeAnimation { get; set; }
    public int Figure { get; set; }
    public bool IsShowFigure { get; set; }
}
