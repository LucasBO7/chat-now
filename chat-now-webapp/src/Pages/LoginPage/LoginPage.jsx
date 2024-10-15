import {
  AsideContainer,
  LayoutGrid,
} from "../../Components/Containers/Containers";
import { LoginForm } from "../../Components/Forms/forms";

export const LoginPage = () => {
  return (
    <AsideContainer>
      <LayoutGrid style={"items-center"}>
        {/* <h1 className="text-white uppercase text-3xl">Login</h1> */}

        <LoginForm />
      </LayoutGrid>
    </AsideContainer>
  );
};
