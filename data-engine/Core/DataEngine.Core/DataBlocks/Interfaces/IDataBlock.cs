﻿using DataEngine.Abstraction.Models;
using System.Threading.Tasks.Dataflow;

namespace DataEngine.Core.DataBlocks.Interfaces;

public interface IDataBlock : IDataflowBlock
{
    string Id { get; set; }
    string LinkId { get; set; }
    string Name { get; set; }
    ITargetBlock<IRowDataModel> Target { get;}
    void LinkTo(IDataflowBlock block, DataflowLinkOptions linkOptions);
}
