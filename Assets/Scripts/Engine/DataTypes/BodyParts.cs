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
        Heart = 0x400,
        Arms = 0x800,
        Elbows = 0x1000,
        Palms = 0x2000,
        Neck = 0x4000,
        Ribs = 0x8000,
        Stomach = 0x10000,
        Liver = 0x20000,
        Pancreas = 0x40000,
        Bladder = 0x80000,
        SmallIntestine = 0x100000,
        LargeIntestine = 0x200000,
        ReproductiveTools = 0x400000,
        Buttocks = 0x800000,
        Thighs = 0x1000000,
        Calves = 0x2000000,
        Feet = 0x4000000,
        Cubes = 0x8000000,
        Kidneys = 0x10000000,
        Belly = 0x20000000,
        MultipleOrgans = 0x4000000,
        UpperRespiratoryDays = Mouth | Throat | Larynx | Trachea,
        LowerRespiratoryPathways = Bronchi | Lungs,
        Airways = UpperRespiratoryDays | LowerRespiratoryPathways,
        Hands = Arms | Elbows | Palms,
        Legs = Thighs | Calves | Cubes | Feet,
        Torso = Belly | Ribs,
        DigestivePlant = Stomach | SmallIntestine | LargeIntestine | Liver | Pancreas,
        UrinarySystem = Bladder | Kidneys,
        External = Head | Hands | Torso | Legs | Ears | Nose | Neck | Buttocks,
        Internal = Airways | DigestivePlant | UrinarySystem | ReproductiveTools | Heart | Eyes | Brain | Ribs,
        Any = External | Internal | MultipleOrgans
    }    
}
