using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewSF64Toolkit.DataStructures;
using NewSF64Toolkit.DataStructures.DMA;
using NewSF64Toolkit.DataStructures.DataObjects;

namespace NewSF64Toolkit.Tools.ResourceInfo
{
    public enum ResourceType
    {
        SF64Rom,
        DMAFile,
        LevelDMAFile,
        ReferenceDMAFile,
        DialogueDMAFile,
        HeaderDMAFile,
        DMATableDMAFile,
        RamTableEntry,
        Empty
    }

    public static class ResourceInfoFactory
    {
        public static IResourceInfo ConvertToResourceInfo(object obj)
        {
            if (obj is SF64ROM)
            {
                return new SF64RomInfo((SF64ROM)obj);
            }
            else if (obj is LevelDMAFile)
            {
                return new LevelDMAFileInfo((LevelDMAFile)obj);
            }
            else if (obj is ReferenceDMAFile)
            {
                return new ReferenceDMAFileInfo((ReferenceDMAFile)obj);
            }
            else if (obj is DialogueDMAFile)
            {
                return new DialogueDMAFileInfo((DialogueDMAFile)obj);
            }
            else if (obj is HeaderDMAFile)
            {
                return new HeaderDMAFileInfo((HeaderDMAFile)obj);
            }
            else if (obj is DMATableDMAFile)
            {
                return new DMATableDMAFileInfo((DMATableDMAFile)obj);
            }
            else if (obj is DMAFile) //Keep DMAFile last since all children of DMAFile will return true on this
            {
                return new DMAFileInfo((DMAFile)obj);
            }
            else if (obj is RAMTableEntry)
            {
                return new RAMTableEntryInfo((RAMTableEntry)obj);
            }
            else if (obj is RefSimpleLevelObject)
            {
                return new RefSimpleObjectInfo((RefSimpleLevelObject)obj);
            }
            else if (obj is RefAdvancedLevelObject)
            {
                return new RefAdvancedObjectInfo((RefAdvancedLevelObject)obj);
            }
            else if (obj is SFLevelObject)
            {
                return new SFLevelObjectInfo((SFLevelObject)obj);
            }
            else if (obj is System.Collections.IList)
            {
                return new ListInfo((System.Collections.IList)obj);
            }
            else
            {
                return null;
            }
        }
    }
}
