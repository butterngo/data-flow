type Props = {};

const Output = (props: Props) => {
  const arrs = new Array(100).fill(null);

  return (
    <>
      <div className="overflow-visible sticky top-0 z-10">
        <div className="flex min-w-[1000px] absolute bg-etlHover">
          <div className="flex-1 justify-between text-lg font-bold text-white border-etlBorder border p-1">
            T1
          </div>
          <div className="flex-1 justify-between text-lg font-bold text-white border-etlBorder border p-1">
            T12
          </div>
          <div className="flex-1 justify-between text-lg font-bold text-white border-etlBorder border p-1">
            T13
          </div>
          <div className="flex-1 justify-between text-lg font-bold text-white border-etlBorder border p-1">
            T14
          </div>
        </div>
      </div>
      <div className="min-w-[1000px] absolute top-10 left-0">
        {arrs.map((p, index) => (
          <div className="flex" key={index}>
            <div className="flex flex-1 justify-between text-lg font-bold text-white border-etlBorder border p-1">
              T1
            </div>
            <div className="flex flex-1 justify-between text-lg font-bold text-white border-etlBorder border p-1">
              T12
            </div>{" "}
            <div className="flex flex-1 justify-between text-lg font-bold text-white border-etlBorder border p-1">
              T13
            </div>
            <div className="flex flex-1 justify-between text-lg font-bold text-white border-etlBorder border p-1">
              T14
            </div>
          </div>
        ))}
      </div>
    </>
  );
};

export default Output;
