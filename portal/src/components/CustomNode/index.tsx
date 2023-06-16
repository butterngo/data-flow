import { memo } from "react";
import { Handle, Position } from "reactflow";
import { BlockType } from "../../constants";
import File from "./File";
import CloseSVG from "../Icons/close";
import MenuSVG from "../Icons/menu";
import "./CustomNode.css";
import { Block } from "../../types/Block";

type Props = {
  id: string;
  data: Block;
  onChangeNode?: () => void;
  onDeleteNode?: () => void;
};

const renderBlockItem = (id: string, data: Block) => {
  switch (data.type) {
    case BlockType.FILE:
      return <File data={data} id={id} />;
  }
};

function CustomNode({ data, onChangeNode, onDeleteNode, id }: Props) {
  return (
    <div className="relative flex min-h-[100px]">
      <div className="flex flex-grow flex-col bg-node border border-etlBorder rounded-sm shadow-lg custom-node">
        <div className="flex justify-between items-center border-b border-etlBorder pl-3 pr-3 pt-1">
          <div className="flex justify-between items-center text-white text-lg">
            <MenuSVG />
            File
          </div>
          <div
            onClick={onDeleteNode}
            className="cursor-pointer text-sm text-gray-500 opacity-30 hover:opacity-100 hover:text-white"
          >
            <CloseSVG />
          </div>
        </div>
        <div className="h-full flex-1 p-3 text-white text-sm space-y-2">
          {renderBlockItem(id, data)}
        </div>
      </div>
      <div className="absolute -bottom-1 top-auto left-0 text-xs text-gray-300 translate-y-full min-w-[170px]">
        [DATASET] 195 rows | 4 columns
      </div>
      <Handle
        type="target"
        position={Position.Right}
        className="w-8 h-[calc(100%+2px)] bg-nodeHandle absolute right-[-32px] 
        !rounded-none rounded-r-lg border-etlBorder hover:bg-nodeHandleHover"
      />
    </div>
  );
}

export default memo(CustomNode);
