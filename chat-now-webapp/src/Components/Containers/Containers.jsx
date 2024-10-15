export const AsideContainer = ({ children }) => {
  return <aside className="w-full h-screen bg-dark-purple">{children}</aside>;
};

export const LayoutGrid = ({ children, style }) => {
  return <div className={`flex items-start justify-center w-full h-full px-[10%] ${style}`}>{children}</div>;
};
