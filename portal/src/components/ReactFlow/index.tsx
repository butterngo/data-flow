import { useCallback, useState } from "react";
import ReactFlow, {
  Background,
  BackgroundVariant,
  Controls,
  Node,
  NodeTypes,
  useReactFlow,
} from "reactflow";

import { Button } from "antd";
import BlockModal from "@/components/Blocks";
import CustomNode from "@/components/CustomNode";

import { BlockType } from "@/constants";

const connectionLineStyle = { stroke: "#fff" };
const nodeTypes: NodeTypes = {
  customNode: CustomNode,
};

let nodeId = 0;

const edgeOptions = {
  animated: true,
  style: {
    stroke: "white",
  },
};

const CustomNodeFlow = () => {
  const reactFlowInstance = useReactFlow();
  const [open, setOpen] = useState<boolean>(false);

  const handleOpenBlock = () => {
    setOpen(!open);
  };

  const handleChangeBlock = () => {
    handleOpenBlock();
    onAddBlock();
  };

  const onAddBlock = useCallback(() => {
    const id = `${++nodeId}`;
    const newNode: Node = {
      id,
      position: {
        x: Math.random() * 500,
        y: Math.random() * 300,
      },
      data: {
        type: BlockType.FILE,
      },
      type: "customNode",
    };
    reactFlowInstance.addNodes(newNode);
  }, []);

  return (
    <>
      <ReactFlow
        defaultNodes={[]}
        defaultEdges={[]}
        nodeTypes={nodeTypes}
        connectionLineStyle={connectionLineStyle}
        defaultEdgeOptions={edgeOptions}
        onConnect={(con) => console.log(con)}
      >
        <Background variant={BackgroundVariant.Dots} />
        <Controls />
        <Button
          onClick={handleOpenBlock}
          className="absolute rounded-full text-white font-bold z-10 top-3 left-3 hover:bg-etlHover"
          size="large"
        >
          + block
        </Button>
      </ReactFlow>
      <BlockModal
        isOpen={open}
        title="Block Library"
        onClose={handleOpenBlock}
        onChange={handleChangeBlock}
      />
    </>
  );
};

export default CustomNodeFlow;
