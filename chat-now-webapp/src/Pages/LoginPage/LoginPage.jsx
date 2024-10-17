import { useEffect, useState } from "react";
import {
  AsideContainer,
  LayoutGrid,
} from "../../Components/Containers/Containers";
import { LoginForm } from "../../Components/Forms/forms";
import api from "../../Services/Service";

export const LoginPage = () => {
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
      .then((response) => console.log(`FUNCIONOU! ${response}`))
      .catch((error) => alert(error));
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
