import { useEffect, useState } from "react";
import { Card } from "../../Components/Cards/Cards";
import {
  AsideContainer,
  CardsContainer,
  LayoutGrid,
} from "../../Components/Containers/Containers";
import { Header } from "../../Components/Header";
import { TextInput } from "../../Components/Inputs/inputs";
import api from "../../Services/Service";
import { useNavigate } from "react-router-dom";
import { IoIosSearch } from "react-icons/io";

export const HomePage = () => {
  const navigate = useNavigate();

  const [loggedUser, setLoggedUser] = useState(JSON.parse(localStorage.getItem("user")));
  const [chatUsers, setChatUsers] = useState([]);
  const [conversations, setConversations] = useState([]);
  const [searchUser, setSearchUser] = useState("");

  // Busca todas as conversas (tipo resumos)
  const getAllChats = async () => {
    await api.get(`Conversation/BuscarConversas?userId=${loggedUser.user.id}`)
      .then(response => setChatUsers(response.data))
      .catch(error => alert(error));
  };

  useEffect(() => {
    getAllChats();
  }, []);


  return (
    <AsideContainer>
      <Header userName={loggedUser.user && loggedUser.user.name} />

      <LayoutGrid style={"flex-col !justify-start items-center mt-10"}>
        <TextInput
          placeholder={"Pesquise alguém aqui..."}
          styles={"mb-10"}
          value={searchUser}
          onChange={(input) => setSearchUser(input.target.value)}
          icon={<IoIosSearch className="w-[15%] h-full p-2" />}
        />

        <CardsContainer>
          {chatUsers &&
            chatUsers
              .filter(
                (user) =>
                  user &&
                  user.friendName.toLowerCase().includes(searchUser.toLowerCase())
              )
              .map((user, index) =>
                <Card
                  key={index}
                  userImg={user.friendPhotoUrl}
                  userName={user.friendName}
                  onClick={() => navigate("/chat", { state: { user: user } })}
                />)
          }
        </CardsContainer>


      </LayoutGrid>
    </AsideContainer>
  );
};
