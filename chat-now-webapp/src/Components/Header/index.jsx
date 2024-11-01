import { MdPerson } from "react-icons/md";
import { LayoutGrid } from "../Containers/Containers";
import { useNavigate } from "react-router-dom";

export const Header = ({ userName }) => {
  const navigate = useNavigate();

  return (
    <header className="w-full h-[90px] bg-light-purple drop-shadow-xl">
      <LayoutGrid style={"flex h-full items-center justify-between"}>
        <p>{userName}</p>

        <button className="h-fit w-fit" onClick={() => navigate("/amigos")}>
          <MdPerson
            className="w-11 h-11 text-dark-purple"
            style={{
              filter: "drop-shadow(-1px 1px 3.2px rgba(0, 0, 0, 0.25))",
            }}
          />
        </button>
      </LayoutGrid>
    </header>
  );
};
