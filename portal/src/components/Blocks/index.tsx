import { Modal } from "antd";
import "./BlockModal.css";
import FileBlock from "./File";

type BlockModal = {
  isOpen: boolean;
  onChange: () => void;
  title: string;
  onClose?: () => void;
};

const BlockModal = (props: BlockModal) => {
  const { isOpen, onChange, title, onClose } = props;

  return (
    <Modal
      title={<h1 className="text-white text-2xl font-extrabold">{title}</h1>}
      centered
      open={isOpen}
      footer={null}
      onCancel={onClose}
      className="block-modal"
      width={1000}
    >
      <div className="relative min-h-[300px] h-[500px] mt-4 overflow-y-auto p-6">
        <div className="grid  gap-4 grid-flow-row grid-cols-1 sm:grid-cols-3 ">
          <FileBlock onChange={onChange} />
          <FileBlock onChange={onChange} />
          <FileBlock onChange={onChange} />
          <FileBlock onChange={onChange} />
        </div>
      </div>
    </Modal>
  );
};

export default BlockModal;
