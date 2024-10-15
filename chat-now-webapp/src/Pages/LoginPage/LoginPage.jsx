import { useEffect, useState } from "react";
import {
  AsideContainer,
  LayoutGrid,
} from "../../Components/Containers/Containers";
import { LoginForm } from "../../Components/Forms/forms";


export const LoginPage = () => {
  const [user, setUser] = useState({
    name: "",
    email: "",
    password: "",
    confirmPassword: "",
    googleId: ""
  });

  useEffect(() => {
    console.log(user);
  }, [user])

  return (
    <AsideContainer>
      <LayoutGrid style={"items-center"}>
        <LoginForm user={user} setUser={setUser} />
      </LayoutGrid>
    </AsideContainer>
  );
};
