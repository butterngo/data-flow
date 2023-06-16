type Props = {
  onChange: () => void;
};

const FileBlock = ({ onChange }: Props) => {
  return (
    <div
      onClick={onChange}
      className="flex flex-col 
            gap-4 p-2 rounded-lg bg-etlBlock
            border border-etlBlock shadow-lg
            cursor-pointer
            transition ease-in-out delay-150 hover:scale-[1.01] duration-300 hover:border-etlHover"
    >
      <h3 className="text-white font-bold text-xl">File</h3>
      <p className="text-gray-200 text-lg">
        Handles csv, json, geojson or topojson files.
      </p>
      <p className="text-gray-400 text-sm">Input: -</p>
      <p className="text-gray-400 text-sm">Output: Dataset, Geojson</p>
    </div>
  );
};

export default FileBlock;
