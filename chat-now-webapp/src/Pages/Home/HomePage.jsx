import { useEffect, useState } from "react";
import { Card } from "../../Components/Cards/Cards";
import { AsideContainer, CardsContainer, LayoutGrid } from "../../Components/Containers/Containers";
import { Header } from "../../Components/Header";
import { TextInput } from "../../Components/Inputs/inputs";
import api from "../../Services/Service";

export const HomePage = () => {
    const [chatUsers, setChatUsers] = useState(null);
    const [searchUser, setSearchUser] = useState("");

    const getAllChats = async () => {
        await api.get("Users")
            .then(response => setChatUsers(response.data))
            .catch(error => console.log(error));
    }

    useEffect(() => {
        getAllChats();
    }, [])

    return (
        <AsideContainer>
            <Header />

            <LayoutGrid style={"flex-col !justify-start items-center mt-10"}>
                <TextInput
                    placeholder={"Pesquise alguém aqui..."}
                    styles={"mb-10"}
                    value={searchUser}
                    onChange={input => setSearchUser(input.target.value)}
                />

                <CardsContainer>
                    {chatUsers &&
                        chatUsers.filter(user => user && user.Name.toLowerCase().includes(searchUser.toLowerCase()))
                            .map((user, index) => (
                                <Card
                                    key={index}
                                    userName={user.Name}
                                />
                            ))
                    }
                </CardsContainer>

            </LayoutGrid>
        </AsideContainer>
    );
}