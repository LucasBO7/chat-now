import { useEffect, useState } from "react";
import {
  AsideContainer,
  LayoutGrid,
} from "../../Components/Containers/Containers";
import { RegisterForm } from "../../Components/Forms/forms";
import api from "../../Services/Service";

export const RegisterPage = () => {
  const [user, setUser] = useState({
    name: "",
    photoUrl: null,
    email: "",
    password: "",
    confirmPassword: "",
    googleId: "",
  });

  const signUpUser = async (event) => {
    event.preventDefault();

    await api
      .post("User/CadastroEmail", {
        name: user.name,
        photoUrl: null,
        email: user.email,
        password: user.password,
        googleId: null,
      })
      .then((response) => console.log(`FUNCIONOU! ${response}`))
      .catch((error) => alert(error));
  };

  const signUpWithGoogle = async () => {
    await api
      .post("User/CadastroComGoogle", {
        name: user.name,
        googleId: user.googleId,
      })
      .then((response) => console.log(`FUNCIONOU! ${response}`))
      .catch((error) => console.log(error));
  };

  return (
    <AsideContainer>
      <LayoutGrid style={"items-center"}>
        <RegisterForm
          signUpWithGoogle={signUpWithGoogle}
          user={user}
          setUser={setUser}
          signUpUser={signUpUser}
        />
      </LayoutGrid>
    </AsideContainer>
  );
};
