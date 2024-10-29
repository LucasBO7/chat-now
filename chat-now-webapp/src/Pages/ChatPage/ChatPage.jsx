import { useEffect, useState } from "react";
import { AsideContainer, CardsContainer, LayoutGrid } from "../../Components/Containers/Containers";
import { Header } from "../../Components/Header";
import { useLocation, useNavigate } from "react-router-dom";
import api from "../../Services/Service";
import { ContactMessage, MyMessage } from "../../Components/Messages/Messages";
import DefaultPhoto from "../../assets/images/defaultPhoto.png";
import { Input } from "@material-tailwind/react";
import { TextInput } from "../../Components/Inputs/inputs";
import { IoSend } from "react-icons/io5";

export const ChatPage = () => {
    // Usuário logado
    const [loggedUser, setLoggedUser] = useState(JSON.parse(localStorage.getItem("user")));

    // Navegação
    const navigate = useNavigate();
    // Dados do usuário remetente
    const location = useLocation();
    const { user } = location.state;
    // Lista de mensagens
    const [messages, setMessages] = useState([]);
    // Estado do input
    const [message, setMessage] = useState();

    // Verifica se a mensagem é minha
    const isMessageMine = (messageId, loggedUserId) => {
        return loggedUserId === messageId;
    }

    const sendMessage = () => {
        api.post("Message", {
            conversationId: user.idConversation,
            senderId: loggedUser.user.id,
            content: message
        })
            .catch(error => alert(error));
    }

    useEffect(() => {
        api.get(`Message/BuscarMensagensConversa?idConversation=${user.idConversation}`)
            .then(response => setMessages(response.data))
            .catch(error => alert("Não foi possível  buscar as mensagens! " + error))
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

                <div className="flex self-start pb-11 pt-4 gap-6 items-center">
                    <img className="w-11 h-11 rounded-2xl" src={user.friendPhotoUrl ? user.friendPhotoUrl : DefaultPhoto} />

                    <h1 className="text-white font-NotoThai font-bold text-lg">{user.friendName}</h1>
                </div>

                <CardsContainer style="!flex-col !h-[545px]">
                    {
                        messages != [] && (
                            messages.map((message, index) => {
                                let formattedTime = new Date(message.sentTime);
                                formattedTime = formattedTime.toLocaleTimeString('pt-br');

                                return (
                                    isMessageMine(message.senderId, loggedUser.user.id) === true
                                        ? <MyMessage
                                            index={index}
                                            message={message.content}
                                            sendTime={formattedTime}
                                        />
                                        : <ContactMessage
                                            index={index}
                                            message={message.content}
                                            sendTime={formattedTime}
                                        />
                                );
                            })
                        )
                    }
                </CardsContainer>

                <TextInput
                    styles={"px-4"}
                    icon={
                        <IoSend
                            className="h-6 w-6 text-dark-purple"
                            onClick={sendMessage}
                        />}
                    value={message}
                    placeholder={"Insira uma mensagem..."}
                    onChange={event => setMessage(event.target.value)}
                />
            </LayoutGrid>
        </AsideContainer>
    );
}