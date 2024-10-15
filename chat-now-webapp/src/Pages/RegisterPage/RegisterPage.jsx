import { useEffect, useState } from "react";
import {
  AsideContainer,
  LayoutGrid,
} from "../../Components/Containers/Containers";
import { RegisterForm } from "../../Components/Forms/forms";

export const RegisterPage = () => {
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
        <RegisterForm 
            user={user} 
            setUser={setUser}
        />
      </LayoutGrid>
    </AsideContainer>
  );
};
