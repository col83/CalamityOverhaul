﻿using CalamityOverhaul.Content;
using CalamityOverhaul.Content.NPCs.Core;
using CalamityOverhaul.Content.RemakeItems.Core;
using CalamityOverhaul.Content.TileModify.Core;
using System.IO;
using Terraria.ModLoader;

namespace CalamityOverhaul
{
    public enum CWRMessageType : byte
    {
        TungstenRiot,
        OverBeatBack,
        NPCOverrideAI,
        NPCOverrideOtherAI,
        ModifiIntercept_InGame,
        ModifiIntercept_EnterWorld_Request,
        ModifiIntercept_EnterWorld_ToClient,
        NPCbasicData,
        KillTileEntity,
    }

    public class CWRNetWork : ICWRLoader
    {
        public static void HandlePacket(Mod mod, BinaryReader reader, int whoAmI) {
            CWRMessageType type = (CWRMessageType)reader.ReadByte();

            if (type == CWRMessageType.OverBeatBack) {
                CWRNpc.OtherBeatBackReceive(reader, whoAmI);
            }
            else if (type == CWRMessageType.NPCOverrideAI) {
                NPCOverride.NetAIReceive(reader);
            }
            else if (type == CWRMessageType.NPCOverrideOtherAI) {
                NPCOverride.OtherNetWorkReceiveHander(reader);
            }
            else if (type == CWRMessageType.ModifiIntercept_InGame) {
                HandlerCanOverride.NetModifiIntercept_InGame(reader, whoAmI);
            }
            else if (type == CWRMessageType.ModifiIntercept_EnterWorld_Request) {
                HandlerCanOverride.NetModifiInterceptEnterWorld_Server(reader, whoAmI);
            }
            else if (type == CWRMessageType.ModifiIntercept_EnterWorld_ToClient) {
                HandlerCanOverride.NetModifiInterceptEnterWorld_Client(reader, whoAmI);
            }
            else if (type == CWRMessageType.NPCbasicData) {
                NPCSystem.NPCbasicDataHandler(reader);
            }
            else if (type == CWRMessageType.KillTileEntity) {
                TileModifyLoader.HandlerNetKillTE(reader, whoAmI);
            }
        }
    }
}
