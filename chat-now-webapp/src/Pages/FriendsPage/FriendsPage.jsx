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

    const [filteredUsersList, setFilteredUsersList] = useState([]);

    const handleFilterChange = () => {
        filterFriends === true
            ? getFriends()
            : getUsers();

        filterFriends ? setFilterFriends(false) : setFilterFriends(true);
    }

    const handleDelete = async (friendshipId) => {
        await api.delete(`Friendship/RemoverAmigo?idFriendship=${friendshipId}`)
            .then(response => console.log(response.data))
            .catch(error => alert(error));

        getFriends();
        setFilterFriends(true);
    }

    const handleAddFriend = async (receiverId) => {
        await api.post(`Friendship?actualUser=${loggedUser.user.id}&receiverUser=${receiverId}`)
            .then(response => console.log(response))
            .catch(error => console.log(error));

        getFriends();
        setFilterFriends(true);
    }

    // Busca todos os usuários existentes
    const getUsers = async () => {
        // Busca todos os usuários
        const usersResponse = await api.get("User");
        const users = usersResponse.data;

        // Busca somente os amigos
        const friendsResponse = await api.get(`Friendship/Amigos?idUser=${loggedUser.user.id}`);
        const friendsList = friendsResponse.data;


        const usersWithFriendStatus = users.map(user => {
            // Busca o Friend com o mesmo id (se houver)
            const friend = friendsList.find(friend => friend.idUser === user.id);

            return {
                ...user,
                isFriend: friendsList.some(friend => friend.idUser === user.id), // retorna true se houver uma amizade com aquele id, se não, false
                idFriendship: friend ? friend.idFriendship : null // Verifica se "friend" não é null, ou seja, se é amigo
            }
        })

        setUsersList(usersWithFriendStatus);
    }

    //  Busca os amigos do usuário logado
    const getFriends = () => {
        api.get(`Friendship/Amigos?idUser=${loggedUser.user.id}`)
            .then(response => {
                const dataWithFriend = response.data.map(user => ({ ...user, isFriend: true }));

                setUsersList(dataWithFriend);
            })
            .catch(error => alert(error));
    }

    // Carrega os usuários/amigos ao entrar na página 
    useEffect(() => {
        handleFilterChange();
    }, [])

    // Pega a lista de usuários com/sem pesquisa
    useEffect(() => {
        // Filtra a lista se for pesqusiado : pega a lista de usuários
        searchUser ? filterListBySearch() : setFilteredUsersList(usersList);
    }, [searchUser, usersList])

    const filterListBySearch = () => {
        setFilteredUsersList(usersList.filter(user => user.name.toLowerCase().includes(searchUser.toLowerCase())))
    }

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

                <CardsContainer style={"flex-col justify-start max-h-[500px] h-[500px] overflow-y-scroll"}>
                    {
                        filteredUsersList && filteredUsersList.length > 0
                            ? (filteredUsersList
                                .sort((a, b) => b.isFriend - a.isFriend)
                                .map((user, index) =>
                                    <UserCard
                                        key={index}
                                        isFriend={user.isFriend}
                                        user={user}
                                        handleDelete={handleDelete}
                                        handleAddFriend={handleAddFriend}
                                    />
                                )
                            )
                            : <p className="text-white w-full text-center font-Nunito text-lg">Não foi encontrado nenhum usuário!</p>
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