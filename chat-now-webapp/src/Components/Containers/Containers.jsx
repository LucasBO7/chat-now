export const AsideContainer = ({ children }) => {
  return <aside className="w-full h-screen bg-dark-purple">{children}</aside>;
};

export const LayoutGrid = ({ children, style }) => {
  return <div className={`flex items-start justify-center w-full h-full px-[10%] ${style}`}>{children}</div>;
};

export const CardsContainer = ({ children }) => {
  return (
    <div className="flex flex-row flex-wrap w-full gap-3 items-center">{children}</div>
  );
}