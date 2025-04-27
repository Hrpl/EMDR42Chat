using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMDR42Chat.Domain.Commons.DTO;

public class MorphCastDTO
{
    public int Arousal {  get; set; }
    public int Valence { get; set; }
    public int Attention { get; set; }
    public int Angry { get; set; }
    public int Disgust { get; set; }
    public int Fear { get; set; }
    public int Happy { get; set; }
    public int Sad { get; set; }
    public int Surprise { get; set; }
    public int Neutral { get; set; }
    public int Age { get; set; }
}
