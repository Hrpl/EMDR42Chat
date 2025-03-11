using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMDR42Chat.Domain.Commons.DTO;

public class ChatDataDTO
{
    public int SpeedObject { get; set; } = 30;
    public bool SwitchStartObject { get; set; } = false;
    public int QualObjects { get; set; } = 1;
    public int HeightObject { get; set; } = 40;
    public int SizeObject { get; set; } = 35;
    public int TypeAnimation { get; set; } = 1;
    public int Figure { get; set; } = 1;

    public int Color { get; set; } = 1;
    public int ColorCenter { get; set; } = 1;
    public int Background { get; set; } = 1;

    public int SoundMode { get; set; } = 2;

    public int Volume { get; set; } = 100;
    public int Effect { get; set; } = 4;
}
