using System;

namespace Engine
{
    [Flags]
    public enum BodyParts
    {
        Head = 0,
        Eyes = 0x01,
        Ears = 0x02,
        Nose = 0x04,
        Brain = 0x08,
        Mouth = 0x10,
        Larynx = 0x20,
        Throat = 0x40,
        Trachea = 0x80,
        Bronchi = 0x100,
        Lungs = 0x200,
        Heat = 0x400,
        Shoulders = 0x800,
        Elbows = 0x1000,
        Any = Elbows | Shoulders | Heat
        // TODO add new
    }    
}
