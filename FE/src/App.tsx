import ReactFlow from "@/components/ReactFlow";
import Ouput from "@/components/Output";

export default function App() {
  return (
    <div className="flex flex-col h-screen bg-etl">
      <div className="relative min-h-[150px] overflow-auto h-2/3 flex flex-shrink-0">
        <ReactFlow />
      </div>
      <div className="flex flex-gow border-t border-t-etlBorder h-full min-h-min">
        <div className="flex flex-col w-full h-full">
          <div className=" text-white font-bold h-12 border-b border-b-etlBorder p-3">
            OUTPUT
          </div>
          <div className="h-full">
            <div className="relative h-full">
              <Ouput />
            </div>
          </div>
        </div>
        <div className="flex flex-col w-[40%] h-full">
          <div className="text-white font-bold h-12 border-l border-etlBorder border-b border-b-etlBorder p-3">
            LOGS
          </div>
          <div className="h-full border-l border-l-etlBorder"></div>
        </div>
      </div>
    </div>
  );
}
