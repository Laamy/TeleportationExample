using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTeleportationUsage
{
    class GameOffsets
    {
        public static ulong positionOffset = 0x4D0;
    }
    class Game
    {
        public static ulong clientInstance // Game > ClientInstance
        { get => MCM.baseEvaluatePointer(0x041457D8, new ulong[] { 0x0, 0x20 }); } // clientInstance
        public static ulong localPlayer // Game > ClientInstance > LocalPlayer
        { get => MCM.evaluatePointer(clientInstance, new ulong[] { 0xC8, 0x0 }); } // localPlayer
                                                                                   // all this means is Minecraft.Windows.exe+041457D8,0,20,C8,0(LocalPlayerPointer 1.17)
                                                                                   // we put 0x before a hex number to tell the programing language that its hex
                                                                                   // to fix this all you have to do is replace the broken pointer information

        public static Vector3 position // Game > ClientInstance > LocalPlayer > Position
        {
            get
            {
                var vec = new Vector3(0, 0, 0);

                vec.x = MCM.readFloat(localPlayer + GameOffsets.positionOffset); // X Postion
                vec.y = MCM.readFloat(localPlayer + GameOffsets.positionOffset + 4); // Y Postion
                vec.z = MCM.readFloat(localPlayer + GameOffsets.positionOffset + 8); // Z Postion

                return vec;
            }
            set => teleport(value); // call predefined function
        } // Position

        public static void teleport(AABB advancedAxis)
        {
            // Minecraft teleportation is pretty complex for new developers to wrap there head around i know
            // this from personal experience and flash didnt even think it was possible so i had to teach him.

            // minecraft contains two XYZ's aka vector3's the upper and lower position
            // the lower position is your normal minecraft position so lets say its 100,32,100
            // if 100,32,100 is your lower position then your upper one would be 100.6 33.8, 100.6
            // the reason its like this is because its the A-A-B-B(Axis-Aligned Bounding Box)
            // used for minecraft collision detection, so you can use this for phase and noclip aswell!

            MCM.writeFloat(localPlayer + GameOffsets.positionOffset, advancedAxis.x.x); // Lower X Postion
            MCM.writeFloat(localPlayer + GameOffsets.positionOffset + 4, advancedAxis.x.y); // Lower Y Postion
            MCM.writeFloat(localPlayer + GameOffsets.positionOffset + 8, advancedAxis.x.z); // Lower Z Postion

            MCM.writeFloat(localPlayer + GameOffsets.positionOffset + 12, advancedAxis.y.x); // Upper X Postion
            MCM.writeFloat(localPlayer + GameOffsets.positionOffset + 16, advancedAxis.y.y); // Upper Y Postion
            MCM.writeFloat(localPlayer + GameOffsets.positionOffset + 20, advancedAxis.y.z); // Upper Z Postion
        } // Teleportation

        public static void teleport(float x, float y, float z) => teleport(new AABB(new Vector3(x, y, z), new Vector3(x + .6f, y + 1.8f, z + .6f))); // Teleportation

        public static void teleport(Vector3 _Vec3) => teleport(_Vec3.x, _Vec3.y, _Vec3.z); // Teleportation
    }
}
