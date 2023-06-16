import { useState } from "react";
import { Block } from "@/types/Block";
import Upload from "@/components/Icons/upload";

type FileProps = {
  id: string;
  data: Block;
};

const File = ({ id, data }: FileProps) => {
  const idDropFile = `dropzone-file-${id}`;

  const [file, setFile] = useState<File>();

  const dropHandler = (ev: React.DragEvent<HTMLLabelElement>) => {
    ev.preventDefault();

    if (ev.dataTransfer.items) {
      // Use DataTransferItemList interface to access the file(s)
      [...ev.dataTransfer.items].forEach((item) => {
        // If dropped items aren't files, reject them
        if (item.kind === "file") {
          const file = item.getAsFile();
          setFile(file!);
        }
      });
    }
  };

  const handleChangeFile = (e: React.ChangeEvent<HTMLInputElement>) => {
    const files = e.target.files;
    if (files && files.length > 0) {
      setFile(files[0]);
    }
  };

  return (
    <>
      <div className="flex items-center justify-center w-full">
        {file ? (
          <h3 className="text-white text-sm">{file.name}</h3>
        ) : (
          <label
            htmlFor={idDropFile}
            className="flex 
            flex-col items-center justify-center w-full min-h-min border-2
          border-gray-300 border-dashed rounded-lg cursor-pointer
          bg-gray-50 hover:bg-gray-100"
            onDrop={dropHandler}
            onDragOver={(e) => e.preventDefault()}
          >
            <div className="flex flex-col items-center justify-center p-3">
              <Upload />
              <p className="mb-2 text-sm text-gray-500">
                <span className="font-semibold">Click to upload</span> or drop
                file here
              </p>
              <p className="text-xs text-gray-500">
                Allowed types: csv, json, geojson, topojson
              </p>
            </div>
            <input
              id={idDropFile}
              type="file"
              className="hidden"
              onChange={handleChangeFile}
            />
          </label>
        )}
      </div>
    </>
  );
};

export default File;
