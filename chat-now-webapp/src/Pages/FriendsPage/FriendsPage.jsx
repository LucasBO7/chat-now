import { useEffect, useState } from "react";
import { AsideContainer, CardsContainer, LayoutGrid } from "../../Components/Containers/Containers";
import { Header } from "../../Components/Header";
import { TextInput } from "../../Components/Inputs/inputs";
import { UserCard } from "../../Components/Cards/Cards";
import api from "../../Services/Service";
import { useNavigate } from "react-router-dom";

export const FriendsPage = () => {
    const navigate = useNavigate();
    // Pega os dados do usuário logado
    const [loggedUser, setLoggedUser] = useState(JSON.parse(localStorage.getItem("user")));
    const [searchUser, setSearchUser] = useState("");

    const [filterFriends, setFilterFriends] = useState(false);
    const [usersList, setUsersList] = useState(null);

    const handleFilterChange = () => {
        filterFriends
            ? getFriends()
            : getUsers();

        filterFriends ? setFilterFriends(false) : setFilterFriends(true);
    }

    const handleDelete = () => {
        alert("Deletar");
    }

    const handleAddFriend = (receiverId) => {
        api.post(`Friendship?actualUser=${loggedUser.user.id}&receiverUser=${receiverId}`)
            .then(response => console.log(response))
            .catch(error => console.log(error));
    }

    // Busca todos os usuários existentes
    const getUsers = async () => {
        // Busca todos os usuários
        const usersResponse = await api.get("User");
        const users = usersResponse.data;
        console.log('user');
        console.log(users);


        // Busca somente os amigos
        const friendsResponse = await api.get(`Friendship/Amigos?idUser=${loggedUser.user.id}`);
        const friendsList = friendsResponse.data;
        console.log('friends');
        console.log(friendsList);


        const usersWithFriendStatus = users.map(user => ({
            ...user,
            isFriend: friendsList.some(friend => friend.idUser === user.id)
        }))

        setUsersList(usersWithFriendStatus);
    }

    //  Busca os amigos do usuário logado
    const getFriends = () => {
        api.get(`Friendship/Amigos?idUser=${loggedUser.user.id}`)
            .then(response => {
                const dataWithFriend = response.data.map(user => ({ ...user, isFriend: true }));

                setUsersList(dataWithFriend);
                // setUsersList(response.data);
            })
            .catch(error => alert(error));
    }

    useEffect(() => {
        // Carrega os usuários/amigos ao entrar na página 
        handleFilterChange();
    }, [])

    return (
        <AsideContainer>
            <Header userName={loggedUser.user && loggedUser.user.name} />

            <LayoutGrid style={"flex-col !justify-start items-center mt-10"}>
                <p
                    className="self-start mb-4 font-Nunito text-sm font-bold underline uppercase text-white"
                    onClick={() => navigate("/home")}
                >
                    Retornar
                </p>

                <TextInput
                    placeholder={"Pesquise alguém aqui..."}
                    styles={"mb-10"}
                    value={searchUser}
                    onChange={(input) => setSearchUser(input.target.value)}
                />

                <CardsContainer>
                    {
                        usersList
                            ? (usersList.sort((a, b) => b.isFriend - a.isFriend).map((user, index) =>
                                <UserCard
                                    key={index}
                                    isFriend={user.isFriend}
                                    user={user}
                                    handleDelete={handleDelete}
                                    handleAddFriend={handleAddFriend}
                                />
                            ))
                            : <p>Não há nada</p>
                    }
                </CardsContainer>


                <button
                    className="bg-purple w-full h-10 mt-10 rounded-2xl font-NotoThai uppercase font-bold text-lg hover:bg-light-purple"
                    onClick={handleFilterChange}
                >
                    {filterFriends ? "Amigos" : "Todos"}
                </button>

            </LayoutGrid>
        </AsideContainer>
    );
}