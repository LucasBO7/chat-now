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

export const HomePage = () => {
  const [loggedUser, setLoggedUser] = useState(JSON.parse(localStorage.getItem("user")));
  const [chatUsers, setChatUsers] = useState([]);
  const [conversations, setConversations] = useState([]);
  const [searchUser, setSearchUser] = useState("");

  const getAllChats = async () => {
    await api.get(`Friendship/Amigos?idUser=${loggedUser.user.id}`)
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
        />

        <CardsContainer>
          {chatUsers &&
            chatUsers
              .filter(
                (user) =>
                  user &&
                  user.name.toLowerCase().includes(searchUser.toLowerCase())
              )
              .map((user, index) => <Card key={index} userName={user.name} />)
          }
        </CardsContainer>
      </LayoutGrid>
    </AsideContainer>
  );
};
