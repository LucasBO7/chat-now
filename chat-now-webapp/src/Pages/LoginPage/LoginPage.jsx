import { useEffect, useState } from "react";
import {
  AsideContainer,
  LayoutGrid,
} from "../../Components/Containers/Containers";
import { LoginForm } from "../../Components/Forms/forms";
import api from "../../Services/Service";
import { useNavigate } from "react-router-dom";

export const LoginPage = () => {
  const navigate = useNavigate();
  const [user, setUser] = useState({
    name: "",
    email: "",
    password: "",
    confirmPassword: "",
    googleId: "",
  });

  const signInUser = async (event) => {
    event.preventDefault();

    await api
      .post("Login", {
        email: user.email,
        password: user.password,
      })
      .then((response) => {
        // Salva o usuário logado juntamente ao seu token de autenticação no localstorage
        saveUserLocalStorage(response.data);
        // Feedback visual de processo concluído
        alert("Salvo!");
        // Redireciona para a tela Home
        navigate("/home");
      })
      .catch((error) => alert(error));
  };

  // Salva os dados do usuário no localstorage
  const saveUserLocalStorage = (userData) => {
    localStorage.setItem("user", JSON.stringify(userData));
  };

  const signInWithGoogle = async (event) => {
    event.preventDefault();

    await api
      .post("Login/LoginComGoogle", {
        name: user.name,
        googleId: user.googleId,
      })
      .then((response) => console.log(response))
      .catch((error) => console.log(error));
  };

  useEffect(() => {
    console.log(user);
  }, [user]);

  return (
    <AsideContainer>
      <LayoutGrid style={"items-center"}>
        <LoginForm
          signInWithGoogle={signInWithGoogle}
          onSubmit={signInUser}
          user={user}
          setUser={setUser}
        />
      </LayoutGrid>
    </AsideContainer>
  );
};
